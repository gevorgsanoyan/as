using ASFront.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASFront.ModelsView
{
    public class AgroAssetIncomeNormativesListViewModel
    {

        private static int _count = 12;

        private ApplicationDbContext db = new Models.ApplicationDbContext();


        private List<AgroAssetIncomeNormative> _AgroNormatives = new List<AgroAssetIncomeNormative>();


        public List<AgroAssetIncomeNormative> AgroNormatives
        {
            get {
                return _AgroNormatives;
            }


            set {
                _AgroNormatives = value;
            }
        }



      
        
        public AgroAssetIncomeNormativesListViewModel()
        {
            _AgroNormatives = new List<AgroAssetIncomeNormative>();

            for (int i = 1; i <= _count; i++)
            {
                AgroAssetIncomeNormative norm = new AgroAssetIncomeNormative();
                norm.Month = i;
                
                _AgroNormatives.Add(norm);
            }

        }

       

        public AgroAssetIncomeNormativesListViewModel(int BrancheId, int AgroAssetTypesId)
        {

            this.BrancheId = BrancheId;
            this.AgroAssetTypesId = AgroAssetTypesId;

            List<AgroAssetIncomeNormative> normList = db.AgroAssetIncomeNormative.Where(p => (p.BrancheId == BrancheId && p.AgroAssetTypesId == AgroAssetTypesId)).OrderBy(p => p.Month).ToList();


            _AgroNormatives = new List<AgroAssetIncomeNormative>();


            if (normList.Count > 0)
            {



                for (int i = 1; i <= _count; i++)
                {
                    AgroAssetIncomeNormative norm = new AgroAssetIncomeNormative();
                    norm.Month = i;
                    norm.BrancheId = BrancheId;
                    norm.AgroAssetTypesId = AgroAssetTypesId;

                    AgroAssetIncomeNormative item = normList.Where(p => p.Month == i).FirstOrDefault();



                    if (item !=null)
                    {

                        norm = item;
                        
                    }

                    _AgroNormatives.Add(norm);

                }





            }
            else
            {
                for (int i = 1; i <= _count; i++)
                {
                    AgroAssetIncomeNormative norm = new AgroAssetIncomeNormative();
                    norm.Month = i;
                    norm.BrancheId = BrancheId;
                    norm.AgroAssetTypesId = AgroAssetTypesId;

                    _AgroNormatives.Add(norm);
                }
            }



        }


      



   


        public int BrancheId { get; set; }
        public int AgroAssetTypesId { get; set; }



      


      


    }
}