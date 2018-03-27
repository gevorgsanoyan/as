using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using ASFront.Models;

namespace ASFront.ModelsView
{
    public class clientsGroupDetView
    {
        //private static ApplicationDbContext db = new ApplicationDbContext();    
        ApplicationDbContext db = new ApplicationDbContext();
        public clientsGroupDetView() { }

        public clientsGroupDetView(clientsGroup item) : this()
        {
            if (item != null && item.groupId > 0)
            {




                this.clientsGroupId = item.clientsGroupId;
                this.groupId = item.groupId;
                this.SingleClientId = item.clientId;

                this.relType = item.relType;



                this.SingleClientId = item.clientId;


                this.AcraLastRequestDate = item.AcraLastRequestDate;
                this.CreditLoadAcra = item.CreditLoadAcra;
                this.OverdueMoney = item.OverdueMoney;
                this.OverdueDaysCount = item.OverdueDaysCount;
                this.Income = item.Income;





                var clientsGroupItem = (from cg in db.clientsGroup
                                        where cg.groupId == groupId && cg.clientId == item.clientId
                                        join r in db.releationType on cg.relType equals r.releationTypeId
                                        join c in db.clients on cg.clientId equals c.clientId
                                        join g in db.@group on cg.groupId equals g.groupId
                                        select new
                                        {



                                            rt = r.relType,
                                            c.clientName,
                                            c.clientLastName,
                                            g.gruopFullName


                                        }).FirstOrDefault();




                this.rTypeName = clientsGroupItem.rt;

                this.groupName = clientsGroupItem.gruopFullName;
                this.rpFirstName = clientsGroupItem.clientName;
                this.rpLastName = clientsGroupItem.clientLastName;

            }

        }


        static public List<clientsGroupDetView> GetClientsGroupDetViewList(long groupId = 0)
        {
            ApplicationDbContext dbs = new ApplicationDbContext();
            List<clientsGroupDetView> cgList = new List<ModelsView.clientsGroupDetView>();



            List<clientsGroup> clientsGroup = new List<Models.clientsGroup>();

            if (groupId > 0)
            {





                cgList = (from cg in dbs.clientsGroup
                          where cg.groupId == groupId
                          join r in dbs.releationType on cg.relType equals r.releationTypeId
                          join c in dbs.clients on cg.clientId equals c.clientId
                          join g in dbs.@group on cg.groupId equals g.groupId
                          select new clientsGroupDetView
                          {
                              clientsGroupId = cg.clientsGroupId,
                              groupId = cg.groupId,
                              SingleClientId = cg.clientId,

                              relType = cg.relType,


                              rTypeName = r.relType,
                              rpFirstName = c.clientName,
                              rpLastName = c.clientLastName,
                              groupName = g.gruopFullName,


                              AcraLastRequestDate = cg.AcraLastRequestDate,
                              CreditLoadAcra = cg.CreditLoadAcra,
                              OverdueMoney = cg.OverdueMoney,
                              OverdueDaysCount = cg.OverdueDaysCount,
                              Income = cg.Income

                          }).ToList();

                //var grTots;
                //foreach (var cg in cgList)
                //{
                //    if(cg.AcraLastRequestDate == null)
                //    {
                //        grTots = dbs.clientsGroup.Where(c => c.clientId == cg.SingleClientId).FirstOrDefault();
                //        var acraHist = dbs.acras;
                //        grTots.AcraLastRequestDate = en.reqDate;
                //        grTots.CreditLoadAcra = LoansTotSum;
                //        grTots.OverdueMoney = en.totOverdueAMD + Classes.CommonFunction.GetExchRate(2, DateTime.Now) * en.totOverdueUSD;
                //        grTots.OverdueDaysCount = ovrdDaysCount;
                //        grTots.Income = db.clientWorkDatas.Where(w => w.clientId == clientId).Select(s => s.salary).SingleOrDefault()
                //            + db.clientWorkDatas.Where(w => w.clientId == clientId).Select(s => s.otherIncome).SingleOrDefault();


                //        db.SaveChanges();
                //    }
                //}

                

                //clientsGroup = dbs.clientsGroup.Where(p => p.groupId == groupId).ToList();
                //foreach (var cg in clientsGroup)
                //{
                //    clientsGroupDetView cgView = new clientsGroupDetView(cg);

                //    cgList.Add(cgView);
                //}


            }

            return cgList;
        }



        [Display(Name = "ID")]
        public long clientsGroupId { get; set; }



        [Display(Name = "Խումբ Id")]
        public long groupId { get; set; }


        [Display(Name = "Խումբ")]
        public string groupName { get; set; }



        [Display(Name = "Անուն")]
        public string rpFirstName { get; set; }


        [Display(Name = "Ազգանուն")]
        public string rpLastName { get; set; }



        [Display(Name = "Փոխկապակցվածություն")]
        public string rTypeName { get; set; }

        [Display(Name = "Փոխկապակցվածություն")]
        public int? relType { get; set; }





        [Display(Name = "client")]
        public long SingleClientId { get; set; }






        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]


        [Display(Name = "Հարցման վերջին ամսաթիվ")]
        public DateTime? AcraLastRequestDate { get; set; } = null;





        [Display(Name = "Վարկային բեռ/ԱՔՌԱ")]
        public double? CreditLoadAcra { get; set; }




        [Display(Name = "Ժամկետանց գումար")]
        public double? OverdueMoney { get; set; }




        [Display(Name = "Ժամկետանց օրերի քանակ")]
        public int? OverdueDaysCount { get; set; }






        [Display(Name = "Եկամուտ")]
        public double? Income { get; set; }





        [Display(Name = "ԱՔՌԱ հարցման թույլտվություն")]
        public bool AcraRequestAllow { get; set; }




    }
    public class clientsGroupView
    {
        [Display(Name = "Խումբ")]
        public long groupId { get; set; }



        [Display(Name = "Անուն")]
        public string rpFirstName { get; set; }


        [Display(Name = "Ազգանուն")]
        public string rpLastName { get; set; }


        [Display(Name = "Սոց.")]
        public string rpSoc { get; set; }



        [Display(Name = "Փոխկապակցվածություն")]
        public int relType { get; set; }



    }


    public class cgTotalView
    {
        [Display(Name = "Խումբ")]
        public string groupName { get; set; }


        public long groupId { get; set; }


        [Display(Name = "Խմբի անդամներ")]
        public List<clientsGroupView> gMmebers { get; set; }

        [Display(Name = "Հաճախորդ")]
        public long SingleClientID { get; set; }
    }




    public class GuarantorsTotalView
    {
       

        [Display(Name = "Երաշխավորներ")]
        public List<clientsGuarantorView> gMmebers { get; set; }

        [Display(Name = "Հաճախորդ")]
        public long SingleClientID { get; set; }


        [Display(Name = "Հայտ")]
        public long ApplicationID { get; set; }

    }




    public class clientsGuarantorView
    {
        



        [Display(Name = "Անուն")]
        public string rpFirstName { get; set; }


        [Display(Name = "Ազգանուն")]
        public string rpLastName { get; set; }


        [Display(Name = "Սոց.")]
        public string rpSoc { get; set; }



        [Display(Name = "Եկամուտ")]
        public double Income { get; set; }



    }

}