using System;
using System.Collections.Generic;
//using System.Data.Entity.Core.Metadata.Edm;
//using System.Data.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.Entity.Core.Metadata.Edm;
using ASFront.ModelsLinq;
using System.Data;
using ASFront.Models;
using Microsoft.VisualBasic;


namespace ASFront.Classes
{

    class CommonFunction
    {


        public static List<string> GetRolesForEditing()
        {
            List<string> rtVal = new List<string>();
            ApplicationDbContext db = new Models.ApplicationDbContext();

            var rolesEdit = db.applicationEditRoles.Where(r => r.canEdit == true).ToList();

            foreach(var re in rolesEdit)
            {
                rtVal.Add(re.RoleName);
            }
                        
            return rtVal;
        }

        public static int isApplicationEditable(long appId, string userId, List<string> rolesForEditing)
        {
            int rtVal = 0;
            string roleId = "";
            string roleName = "";
            ApplicationDbContext db = new Models.ApplicationDbContext();
            int appStatus = db.applications.Where(a => a.applicationId == appId).Select(s => s.appStatus).SingleOrDefault();
            string appUser = db.applications.Where(a => a.applicationId == appId).Select(s => s.userId).SingleOrDefault();
            var uroles = db.Users.Where(u => u.Id == userId).Select(s => s.Roles).ToList();
            foreach(var ur in uroles)
            {
                roleId = ur.Select(s => s.RoleId).SingleOrDefault();                
            }
            roleName = db.Roles.Where(r => r.Id == roleId).Select(n => n.Name).SingleOrDefault();

            if (appStatus == 1)
            {
                if (rolesForEditing.Contains(roleName))
                    rtVal = 1;
            }//if(appStatus == 1)

            return rtVal;
        }

        public static double CalculateCreditLoadAcra(long ApplicationID = 0)
        {
            ApplicationDbContext db = new Models.ApplicationDbContext();
            long ClientID = db.applications.Where(p => p.applicationId == ApplicationID).Select(p => p.clientId).FirstOrDefault();




            double credit = 0;



            if (ClientID > 0)
            {


                var alList = db.acraLoans.Where(c => c.clientId == ClientID && c.lType == "Վարկ").ToList();



                foreach (var al in alList)
                {
                    if (al != null)
                    {


                        credit += CalculateBalanceCurrency(al);


                    }
                }





            }



            return credit;

        }




        public static double GetSefLoansSumFromACRAProfile(long clientId, string creditSource)
        {
            double sefLoanSum = 0;

            ApplicationDbContext db = new ApplicationDbContext();

            var sefLoans = db.acraLoans.Where(l => l.clientId == clientId && l.cCreditSource == creditSource && l.balance != 0);
            DateTime dt;
            foreach (var sl in sefLoans)
            {
                try {
                    dt = new DateTime(sl.CreditStart.Value.Year, sl.CreditStart.Value.Month, sl.CreditStart.Value.Day);
                }
                catch
                {
                    dt = DateTime.Now;
                }

                sefLoanSum += sl.balance * GetExchRate(sl.currencyId, dt);
            }
            

            return sefLoanSum;
        }//public static long GetSefLoansSumFromACRAProfile(long clientId)






        public static int GetAppStatus(int ScoringDecisionID, ProductLimits pl = null, ApplicationAppruves ApplicationAppruvesR1 = null, ApplicationAppruves ApplicationAppruvesR2 = null)
        {
            int _returnValue = 1;

            if (ScoringDecisionID == 5)
            {
                _returnValue = 5;
            }
            else
            {
                if (pl != null)
                {
                    bool Scoring = pl?.Scoring ?? false;
                    bool App1 = pl?.App1 ?? false;
                    bool App2 = pl?.App2 ?? false;



                    if (Scoring && App1 && App2)
                    {
                        if (ScoringDecisionID == 5 || ApplicationAppruvesR1?.aprStatus == 5 || ApplicationAppruvesR2?.aprStatus == 5)
                        {
                            _returnValue = 5;
                        }
                        else
                        if (ScoringDecisionID == 4 && ApplicationAppruvesR1?.aprStatus == 4 && ApplicationAppruvesR2?.aprStatus == 4)
                        {
                            _returnValue = 4;
                        }

                    }

                    else if (Scoring && App1)
                    {
                        if (ScoringDecisionID == 5 || ApplicationAppruvesR1?.aprStatus == 5)
                        {
                            _returnValue = 5;
                        }
                        else
                          if (ScoringDecisionID == 4 && ApplicationAppruvesR1?.aprStatus == 4)
                        {
                            _returnValue = 3;
                        }
                    }

                    else if (Scoring)
                    {
                        if (ScoringDecisionID == 4)
                        {
                            _returnValue = 2;
                        }
                        else if (ScoringDecisionID == 5)
                        {
                            _returnValue = 5;
                        }
                    }

                }



            }


            return _returnValue;

        }





        #region //IncomeExpenses


        public static void CalculateIncomeExpenses(long ApplicationID)
        {
            ApplicationDbContext db = new Models.ApplicationDbContext();
            long ClientID = db.applications.Where(p => p.applicationId == ApplicationID).Select(p => p.clientId).FirstOrDefault();

            CalculateGeneralInformation(ApplicationID);
            CalculateBusinessData(ApplicationID);
            CalculateAgriculturalData(ApplicationID);
            CalculateFamilyCost(ApplicationID);

        }

        public static void CalculateGeneralInformation(long ApplicationID = 0)
        {
            ApplicationDbContext db = new Models.ApplicationDbContext();
            long groupId = 0;
            double pmt = 0;

            long ClientID = db.applications.Where(p => p.applicationId == ApplicationID).Select(p => p.clientId).FirstOrDefault();

            if (ClientID > 0)
            {


                IncomeExpenses incomeExpenses = db.IncomeExpenses.Where(p => p.applicationId == ApplicationID).OrderByDescending(p => p.Id).FirstOrDefault();
                clientWorkDatas cw = db.clientWorkDatas.OrderByDescending(p => p.Id).Where(p => p.clientId == ClientID).FirstOrDefault();


                if (cw != null)
                {
                    incomeExpenses.ClientsWage = cw.salary + cw.otherIncome;

                }

                db.SaveChanges();


                var clientLoans = db.acraLoans.OrderByDescending(p => p.acraLoansId).Where(p => p.clientId == ClientID && p.creditStatus == "գործող").ToList();
                foreach (var al in clientLoans)
                {

                    if (al != null)
                    {
                        pmt += al.pmt; //CalculatePmtCurrency(al);                                                                    
                    }
                }
                incomeExpenses.LoanInterestExpenses = pmt;

                pmt = 0;

                groupId = db.clientsGroup.Where(c => c.clientId == ClientID).OrderByDescending(p => p.clientsGroupId).Select(g => g.groupId).FirstOrDefault();
                if (groupId > 0)
                {
                    var clList = db.clientsGroup.Where(gr => gr.groupId == groupId && gr.clientId != ClientID).Select(p => p.clientId).Distinct().ToList();

                    foreach (var cl in clList)
                    {
                        
                        var alList = db.acraLoans.OrderByDescending(p => p.acraLoansId).Where(p => p.clientId == cl && p.creditStatus == "գործող").ToList();

                        foreach (var al in alList)
                        {

                            if (al != null)
                            {
                                pmt += al.pmt; //CalculatePmtCurrency(al);                                                                    
                            }
                        }
                    }



                    incomeExpenses.FamilysLoanInterestExpenses = pmt;


                    db.SaveChanges();

                }//if(groupId > 0)





            }//if(EditClientId != "0")





        }
        public static void CalculateBusinessData(long ApplicationID = 0)
        {
            ApplicationDbContext db = new Models.ApplicationDbContext();
            long groupId = 0;

            //var =agr db.AgroAsset.Where(p=> p.applicationId==)


        }
        public static void CalculateAgriculturalData(long ApplicationID = 0)
        {
            ApplicationDbContext db = new Models.ApplicationDbContext();
            double sum = 0;
            double sumExp = 0;

            var app = db.applications.Where(p => p.applicationId == ApplicationID).FirstOrDefault();
            IncomeExpenses incomeExpenses = db.IncomeExpenses.Where(p => p.applicationId == ApplicationID).OrderByDescending(p => p.Id).FirstOrDefault();

            var AgroAssets = db.AgroAsset.Where(p => p.applicationId == ApplicationID).ToList();


            foreach (var AgroAsset in AgroAssets)
            {

                double normAverage = 0;
                double normAverageExpenses = 0;

                var AgroAssetIncomeNormatives = db.AgroAssetIncomeNormative.Where(p => p.BrancheId == app.branchId && p.AgroAssetTypesId == AgroAsset.AgroAssetTypesId).Take(12);

                foreach (var nor in AgroAssetIncomeNormatives)
                {

                    normAverage += nor.Price * nor.Quantity;
                    normAverageExpenses += nor.Expenses;

                }

                normAverage = normAverage / 12;
                normAverageExpenses = normAverageExpenses / 12;

                sum += normAverage * AgroAsset.Quantity;
                sumExp += normAverageExpenses * AgroAsset.Quantity;

            }


            sum = RoudNumber(sum);
            sumExp = RoudNumber(sumExp);

            incomeExpenses.AgroIncome = sum;
            incomeExpenses.AgroExpenses = sumExp;



            db.SaveChanges();


        }



        public static double RoudNumber(double num)
        {
            double returnvalue = 0;
            num = num / 10;
            returnvalue = Math.Round(num) * 10;

            return returnvalue;
        }

        public static void CalculateFamilyCost(long ApplicationID)
        {
            ApplicationDbContext db = new Models.ApplicationDbContext();

            long ClientID = db.applications.Where(p => p.applicationId == ApplicationID).Select(p => p.clientId).FirstOrDefault();



            long groupId = 0;


            double FamilyMembersWages = 0;
            double OtherFamilyIncome = 0;
            double FamilyExpenses = 0;











            if (ClientID > 0)
            {

                groupId = db.clientsGroup.Where(c => c.clientId == ClientID).OrderByDescending(p => p.clientsGroupId).Select(g => g.groupId).FirstOrDefault();
                if (groupId > 0)
                {
                    var clList = db.clientsGroup.Where(gr => gr.groupId == groupId && gr.clientId != ClientID).Select(p => p.clientId).Distinct().ToList();

                    foreach (var cl in clList)
                    {
                        clientWorkDatas cw = db.clientWorkDatas.OrderByDescending(p => p.Id).Where(p => p.clientId == cl).FirstOrDefault();


                        if (cw != null)
                        {
                            FamilyMembersWages += cw.salary;
                            OtherFamilyIncome += cw.otherIncome;
                        }


                        clients client = db.clients.Where(p => p.clientId == cl).FirstOrDefault();

                        if (client != null)
                        {
                            int BrancheId = client.BranchtId;
                            int age = (new DateTime(1, 1, 1) + (DateTime.Now - client.dob)).Year - 1;
                            // int age = Convert.ToInt32(((DateTime.Now - client.dob)).TotalDays / 365.255);

                            FamilyCostNormatives fn = db.FamilyCostNormatives.Where(p => p.BrancheId == BrancheId && p.maxAge >= age).OrderBy(p => p.maxAge).FirstOrDefault();
                            if (fn != null)
                                FamilyExpenses += fn.Cost;
                        }





                    }

                    clients clientL = db.clients.Where(p => p.clientId == ClientID).FirstOrDefault();
                    int ageL = (new DateTime(1, 1, 1) + (DateTime.Now - clientL.dob)).Year - 1;


                    FamilyCostNormatives fnL = db.FamilyCostNormatives.Where(p => p.BrancheId == clientL.BranchtId && p.maxAge >= ageL).OrderBy(p => p.maxAge).FirstOrDefault();
                    if (fnL != null)
                        FamilyExpenses += fnL.Cost;



                    IncomeExpenses incomeExpenses = new IncomeExpenses();
                    incomeExpenses = db.IncomeExpenses.Where(p => p.applicationId == ApplicationID).OrderByDescending(p => p.Id).FirstOrDefault();




                    incomeExpenses.FamilyMembersWages = FamilyMembersWages;
                    incomeExpenses.OtherFamilyIncome = OtherFamilyIncome;
                    incomeExpenses.FamilyExpenses = FamilyExpenses;

                    db.SaveChanges();

                }//if(groupId > 0)





            }//if(EditClientId != "0")



        }




        #endregion //IncomeExpenses

        public static Dictionary<int, double> CurrencyIndexDic = new Dictionary<int, double>()
        {
            { 1, 1}, // N'ՀՀ Դրամ', N'Armenian Dram', N'AMD', N'֏')
            { 2,  480}, // N'ԱՄՆ Դոլար', N'US Dollar', N'USD', N'$')
            { 3,  568 },//N'ՌԴ Ռուբլի', N'RF Rubli', N'RUR', N'₽')
            { 4,  8.20} //N'Եվրո', N'Euro', N'EUR', N'€')

        };






        public static double CalculateBalanceCurrency(acraLoans al)
        {

            double returnvalue = 0;
            ApplicationDbContext db = new Models.ApplicationDbContext();
            double CurrencyIndex = 1;

            if (CurrencyIndexDic.Any(p => p.Key == al.currencyId))
                CurrencyIndex = CurrencyIndexDic[al.currencyId];



            returnvalue = al.balance * CurrencyIndex;

            return returnvalue;
        }




        public static double CalculatePmtCurrency(acraLoans al)
        {

            double returnvalue = 0;
            ApplicationDbContext db = new Models.ApplicationDbContext();
            double CurrencyIndex = 1;

            if (CurrencyIndexDic.Any(p => p.Key == al.currencyId))
                CurrencyIndex = CurrencyIndexDic[al.currencyId];



            returnvalue = al.pmt * CurrencyIndex;

            return returnvalue;
        }


        #region //Balance


        public static void CalculateInventory(long ApplicationID = 0)
        {
            ApplicationDbContext db = new Models.ApplicationDbContext();

            var trS = db.Turnovers.Where(p => p.applicationId == ApplicationID).ToList();
            double sum = 0;
            foreach (var tr in trS)
            {
                sum += tr.COGS * tr.OutstandingQuantity;
            }
            sum = Round(sum);
            Balance balance = db.Balance.Where(p => p.applicationId == ApplicationID).OrderByDescending(p => p.Id).FirstOrDefault();

            balance.Inventory = sum;

            db.SaveChanges();
        }

        public static double Round(double number)
        {
            return Math.Round(number);
        }
        public static void CalculateShortTermLoans(long ApplicationID = 0)
        {
            ApplicationDbContext db = new Models.ApplicationDbContext();
            long groupId = 0;


            double credit = 0;

            long ClientID = db.applications.Where(p => p.applicationId == ApplicationID).Select(p => p.clientId).FirstOrDefault();

            if (ClientID > 0)
            {

                groupId = db.clientsGroup.Where(c => c.clientId == ClientID).OrderByDescending(p => p.clientsGroupId).Select(g => g.groupId).FirstOrDefault();
                if (groupId > 0)
                {
                    var clList = db.clientsGroup.Where(gr => gr.groupId == groupId).Select(p => p.clientId).Distinct().ToList();

                    foreach (var cl in clList)
                    {
                        var alList = db.acraLoans.OrderByDescending(p => p.acraLoansId).Where(p => p.clientId == cl && p.balance>0 && p.lType == "Վարկ").ToList();



                        foreach (var al in alList)
                        {
                            if (al != null)
                            {
                                double TotalDays = ((al.creditCloseDate - DateTime.Now)).TotalDays;

                                if (TotalDays <= 365)
                                {
                                    credit += CalculateBalanceCurrency(al);
                                }

                            }
                        }



                    }



                    Balance balance = db.Balance.Where(p => p.applicationId == ApplicationID).OrderByDescending(p => p.Id).FirstOrDefault();

                    balance.ShortTermLoans = RoudNumber(credit);

                    db.SaveChanges();

                }//if(groupId > 0)





            }//if(EditClientId != "0")



        }






        public static void CalculateMediumLongTermLoans(long ApplicationID = 0)
        {
            ApplicationDbContext db = new Models.ApplicationDbContext();
            long groupId = 0;


            double credit = 0;

            long ClientID = db.applications.Where(p => p.applicationId == ApplicationID).Select(p => p.clientId).FirstOrDefault();

            if (ClientID > 0)
            {

                groupId = db.clientsGroup.Where(c => c.clientId == ClientID).OrderByDescending(p => p.clientsGroupId).Select(g => g.groupId).FirstOrDefault();
                if (groupId > 0)
                {
                    var clList = db.clientsGroup.Where(gr => gr.groupId == groupId).Select(p => p.clientId).Distinct().ToList();

                    foreach (var cl in clList)
                    {
                        var alList = db.acraLoans.OrderByDescending(p => p.acraLoansId).Where(p => p.clientId == cl && p.lType == "Վարկ").ToList();

                        foreach (var al in alList)
                        {
                            if (al != null)
                            {
                               

                                double TotalDays = ((al.creditCloseDate - DateTime.Now)).TotalDays;

                                if (TotalDays > 365)
                                {
                                    credit += CalculateBalanceCurrency(al);
                                }


                            }

                        }


                    }



                    Balance balance = db.Balance.Where(p => p.applicationId == ApplicationID).OrderByDescending(p => p.Id).FirstOrDefault();

                    balance.MediumLongTermLoans = RoudNumber(credit);

                    db.SaveChanges();

                }//if(groupId > 0)





            }//if(EditClientId != "0")



        }



        #endregion //Balance

        public static double GetExchRate(int currId, DateTime dt)
        {
            double exRate = 1;
            switch (currId)
            {
                case 1:
                    exRate = 1;
                    break;
                case 2:
                    exRate = 480;
                    break;
            }
            return exRate;

        }


        public static double PMTCalc(long hayt_ID, double CreditTotalAmount)

        {
            ApplicationDbContext db = new Models.ApplicationDbContext();

            Models.applications app = db.applications.Where(p => p.applicationId == hayt_ID).FirstOrDefault();

            double totPMT = 0;

            if (app != null)
            {
                int prod_ID = app.productId;
                var pr = db.Products.Where(p => p.productId == app.productId).FirstOrDefault();

                int maxDurat = app.CreditTerm;
                int iNumberOfPayments = maxDurat;
                double mfee = pr.mothFee;
                double anRate = pr.anualRate / 12;
                double upfrontfee = pr.upfronFee;

                try
                {
                    if (iNumberOfPayments != 0)

                        totPMT = (Financial.Pmt(anRate, iNumberOfPayments, CreditTotalAmount, 0, DueDate.EndOfPeriod) * -1) + CreditTotalAmount * mfee;
                }

                catch { }



            }





            return totPMT;
        }

        public static object TrimObject(object obj)
        {


            var stringProperties = obj.GetType().GetProperties()
                                      .Where(p => p.PropertyType == typeof(string));

            foreach (var stringProperty in stringProperties)
            {

                object currentValueObject = stringProperty.GetValue(obj, null);
                if (currentValueObject != null)
                {
                    string currentValue = (string)(currentValueObject);
                    stringProperty.SetValue(obj, currentValue.Trim(), null);
                }

            }

            return obj;
        }


        static public List<DropDownItem> GetTablesNames()
        {
            ASFrontDataContext dbL = new ASFrontDataContext();


            var result = dbL.GetTablesNames().Select(p => new DropDownItem { Name = p.TABLE_NAME, ID = p.TABLE_NAME }).ToList();
            return result;
        }

        static public List<DropDownItem> GetColumnNames(string table)
        {
            ASFrontDataContext dbL = new ASFrontDataContext();


            List<DropDownItem> result = dbL.GetColumnNames(table).Select(p => new DropDownItem { Name = p.Column_Name, ID = p.Column_Name }).ToList();
            return result;
        }

        static public string FormulaFix(string Formula)
        {
            string formulFixed = string.Empty;



            formulFixed = WhiteSpaceFix_FormulaEdit(Formula);

            return formulFixed;

        }

        static public void FixValue()
        {

        }




        public static double getFieldValueDouble(string table, string field, Int64 clientId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            double returnValue = 0;

            string query = $"SELECT TOP 1 {field} FROM dbo.{table} WHERE clientId={clientId} ";

            try
            {
                returnValue = db.Database.SqlQuery<double>(query).FirstOrDefault();
            }
            catch (Exception ex) { }



            return returnValue;
        }



        public static DateTime getFieldValueDateTime(string table, string field, Int64 clientId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            DateTime returnValue = new DateTime();

            string query = $"SELECT {field} FROM dbo.{table}  WHERE clientId={clientId}";

            try
            {

                returnValue = db.Database.SqlQuery<DateTime>(query).FirstOrDefault();
            }
            catch (Exception ex) { }



            return returnValue;
        }


        public static int RegionIdGetFromName(string rname)
        {
            int rVal = 0;

            ApplicationDbContext db = new ApplicationDbContext();
            var reg = from r in db.comunities
                      where r.reg == rname
                      group r by r.reg into g
                      select new
                      {
                          Id = g.Key,
                          regId = g.Min(gi => gi.comunityId)
                      };

            rVal = reg.FirstOrDefault().regId;
            return rVal;
        }




        public static KeyValuePair<string, string> FindParametr(string par, int indicatorID)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            KeyValuePair<string, string> returnValue = new KeyValuePair<string, string>();


            //string table= string.Empty; 
            //    string field   = string.Empty; 
            //string query = $"SELECT {field} FROM dbo.{table}";

            try
            {

                //if (db.ScoringParameters.Any() )
                //{
                //    int ParametrID

                //    if (db.ScoringIndicatorsParameters.Any(p => (p.IndicatorID == indicatorID && p.ParameterID == 1)))
                //    {

                //        table = db.Database.SqlQuery<string>(query).FirstOrDefault();
                //        field = db.Database.SqlQuery<string>(query).FirstOrDefault();
                //    }

                //}





                var rec = (

                    from p in db.ScoringParameters
                    join pi in db.ScoringIndicatorsParameters on p.ID equals pi.ParameterID

                    where (pi.IndicatorID == indicatorID && p.InputParameterName == par)

                    select p
                    ).FirstOrDefault();




                returnValue = new KeyValuePair<string, string>(rec.SourceTable, rec.SourceField);





            }
            catch (Exception ex)
            {

            }





            return returnValue;
        }


        static public string Formula()
        {
            string returnvalue = string.Empty;



            return returnvalue;
        }
        static public string FormulaInsertValue(string str, int indicatorID, Int64 clientId)
        {

            string[] strArray = str.Split(' ');



            string operList = "+-/*=^()";

            string returnvalue = string.Empty;


            foreach (var item in strArray)
            {
                if (!item.StartsWith("#"))
                {

                    if (item.StartsWith("$") && !operList.Contains(item))
                    {

                        KeyValuePair<string, string> val = FindParametr(item.Replace("$", ""), indicatorID);

                        double number = getFieldValueDouble(val.Key, val.Value, clientId);

                        returnvalue += number.ToString();

                    }
                    else
                    {
                        returnvalue += item;

                    }
                }
                else
                {
                    var keys = Methods.MethodsDict.Keys.ToList();
                    foreach (var it in Methods.MethodsDict.Keys)
                    {
                        if (item.Contains(it))
                        {
                            string par = string.Empty;

                            par = item.Replace(it, "").Replace("#", "").Replace("(", "").Replace(")", "").Trim();
                            returnvalue += Methods.Execute(it, par, indicatorID, clientId);
                            break;
                        }
                    }

                }


                returnvalue += " ";
            }


            returnvalue = WhiteSpaceFix(returnvalue);



            return returnvalue;
        }

        public static double CalculateValue(string str)
        {
            double returnValue = 0;


            string outString = string.Empty;


            //MSScriptControl.ScriptControl sc = new MSScriptControl.ScriptControl();
            //sc.Language = "javascript";
            //string expression = "1 + 2 * 7";
            //object result = sc.Eval(expression);
            //outString=result.ToString();
            try
            {

                var result = new DataTable().Compute(str, null);
                outString = result.ToString();


                double.TryParse(outString, out returnValue);
            }
            catch (Exception ex)
            {
                ;
            }

            return returnValue;

        }


        static double GetParameterValue(string str)
        {
            double returnValue = 0;


            ApplicationDbContext db = new ApplicationDbContext();


            var par = db.ScoringParameters.Where(p => p.InputParameterName == str).FirstOrDefault();

            db.Database.ExecuteSqlCommand("TRUNCATE TABLE [TableName]");


            //if (par != null)
            //{

            //}



            return returnValue;

        }


        static public string WhiteSpaceFix_FormulaEdit(string str)
        {
            string s = "+-/*=^()";

            str = Regex.Replace(str, @"\s+", "");


            string returnedFormula = string.Empty;
            string formulFixed = str.Replace("+", " + ").Replace("-", " - ").Replace("/", " / ").Replace("*", " * ").Replace("^", " ^ ").Replace("=", " = ").Replace("(", " ( ").Replace(")", " ) ");

            formulFixed = formulFixed.Trim();

            formulFixed = Regex.Replace(formulFixed, @"\s+", " ");

            bool start = false, end = true;
            foreach (var t in formulFixed)
            {
                if (t == '#' && end)
                {
                    start = true;
                    end = false;
                    returnedFormula += t;
                    continue;
                }
                else
                {

                    if (t == '#' && start)
                    {
                        start = false;
                        end = true;
                    }

                    if (start)
                    {
                        if (t != ' ')
                            returnedFormula += t;
                        continue;
                    }
                    else
                    {
                        returnedFormula += t;
                    }


                }


            }

            //         string sss = formulFixed.Split(
            //" ".ToCharArray(),
            //StringSplitOptions.RemoveEmptyEntries
            //).Join(" ");

            return returnedFormula;
        }

        static public string WhiteSpaceFix(string str)
        {
            string s = "+-/*=^()";

            str = Regex.Replace(str, @"\s+", "");



            string formulFixed = str.Replace("+", " + ").Replace("-", " - ").Replace("/", " / ").Replace("*", " * ").Replace("^", " ^ ").Replace("=", " = ").Replace("(", " ( ").Replace(")", " ) ");

            formulFixed = formulFixed.Trim();

            formulFixed = Regex.Replace(formulFixed, @"\s+", " ");

            bool start = false, end = true;


            //         string sss = formulFixed.Split(
            //" ".ToCharArray(),
            //StringSplitOptions.RemoveEmptyEntries
            //).Join(" ");

            return formulFixed;
        }


        static public Tuple<int, DateTime> GetInfroFromSoc(string socN)
        {
            int gender = 0;
            int dday = 0;
            int mmonth = 0;
            DateTime iDOB = new DateTime();
            string inSocCardNum = socN.Trim();
            string tempStr = inSocCardNum.Substring(0, 2);
            int iYoB = 1920;
            int Zuyg = Convert.ToInt32(tempStr);
            if ((Zuyg >= 11) && (Zuyg <= 41))
            {
                gender = 1;
                dday = Zuyg - 10;
            }
            if ((Zuyg >= 51) && (Zuyg <= 81))
            {
                gender = 2;
                dday = Zuyg - 50;
            }
            tempStr = inSocCardNum.Substring(2, 2);
            Zuyg = Convert.ToInt32(tempStr);
            if ((Zuyg >= 1) && (Zuyg <= 12))
            {
                iYoB = 1900;
                mmonth = Zuyg;
            }
            if ((Zuyg >= 21) && (Zuyg <= 32))
            {
                iYoB = 2000;
                mmonth = Zuyg - 20;
            }
            if (Zuyg == 14)
            {
                iYoB = 1900;
            }
            if (Zuyg == 34)
            {
                iYoB = 2000;
            }
            tempStr = inSocCardNum.Substring(4, 2);
            Zuyg = Convert.ToInt32(tempStr);
            if ((Zuyg >= 0) && (Zuyg <= 99))
            {
                iYoB += Zuyg;
            }
            try
            {
                iDOB = new DateTime(iYoB, mmonth, dday);
            }
            catch
            {
                iDOB = new DateTime(1800, 1, 1);
            }


            return Tuple.Create(gender, iDOB);

        }



        //    static private string GetExp(string str)
        //    {
        //        string outString = "";


        //        //MSScriptControl.ScriptControl sc = new MSScriptControl.ScriptControl();
        //        //sc.Language = "javascript";
        //        //string expression = "1 + 2 * 7";
        //        //object result = sc.Eval(expression);
        //        //outString=result.ToString();


        //        //var result = new DataTable().Compute(str, null);
        //        //outString = result.ToString();

        //        return outString;
        //    }
        //        static private string GetExp0(string str)
        //    {
        //        string outString = "";
        //        Stack<char> operStack = new Stack<char>();

        //        for (int i = 0; i < str.Length; i++)
        //        {
        //            ////if (Char.IsDigit(str[i]))
        //            //    outString += str[i];
        //            //else
        //            {
        //                if (str[i] == ' ')
        //                    continue;

        //                else if (IsDelim(str[i]))
        //                {
        //                    if (operStack.Count > 0)
        //                        if (GetPriority(str[i]) <= GetPriority(operStack.Peek()))
        //                            outString += operStack.Pop();

        //                    operStack.Push(str[i]);
        //                }
        //                else if (str[i] == '(')
        //                    operStack.Push(str[i]);

        //                else if (str[i] == ')')
        //                {
        //                    char s = operStack.Pop();

        //                    while (s != '(')
        //                    {
        //                        outString += s;
        //                        s = operStack.Pop();
        //                    }
        //                }


        //            }

        //        }

        //        while (operStack.Count > 0)
        //            outString += operStack.Pop();

        //        return outString;
        //    }

        //    static private bool IsDelim(char с)
        //    {
        //        if (("+-/*=^".IndexOf(с) != -1))
        //            return true;
        //        return false;
        //    }

        //    static private byte GetPriority(char s)
        //    {
        //        switch (s)
        //        {
        //            case '(':
        //                return 0;
        //            case ')':
        //                return 1;
        //            case '+':
        //                return 2;
        //            case '-':
        //                return 3;
        //            case '*':
        //                return 4;
        //            case '/':
        //                return 4;
        //            case '^':
        //                return 5;
        //            default:
        //                return 6;
        //        }
        //    }
        //}


    }
}