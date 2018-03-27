using ASFront.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASFront.ModelsView
{
    public class GuarantorsViewModel
    {


        public GuarantorsViewModel() { }

        public GuarantorsViewModel(clients item) : this()
        {
            this.Id = item.clientId;

            this.fName = item.clientName;
            this.lName = item.clientLastName;
            this.socNumb = item.socNumb;
             this.Income    = item.clientWorkDatas.OrderByDescending(p => p.Id).Select(p => p.salary).FirstOrDefault();


        }




        public  static  List<GuarantorsViewModel>   GetGuarantorsViewModelList(long ApplicationID = 0) 
        {


            ApplicationDbContext dbs = new ApplicationDbContext();
            List<GuarantorsViewModel> cgList = new List<ModelsView.GuarantorsViewModel>();



            cgList = (from cl in dbs.clients
                
                      join gar in dbs.Guarantors on cl.clientId equals gar.clientId
                      where gar.applicationId == ApplicationID
                      
                      select new GuarantorsViewModel
                      {
                           Id = cl.clientId,

            fName = cl.clientName,
            lName = cl.clientLastName,
            socNumb = cl.socNumb,
                          Income = dbs.clientWorkDatas.Where(p => p.clientId == cl.clientId).OrderByDescending(p => p.Id).Select(p => p.salary).FirstOrDefault()
                      }





                ).ToList();



            return cgList;


        }



        [Display(Name = "ID")]
        public long Id { get; set; }

        [Display(Name = "Անուն")]
        public string fName { get; set; }





        [Display(Name = "Ազգանուն")]
        public string lName { get; set; }




        [StringLength(250)]
        [Display(Name = "Սոց. քարտ")]
        [Required(ErrorMessage = "Տվյալ դաշտը պարտադիր է լրացման համար")]
        [MaxLength(10, ErrorMessage = "առավելագույնը 10 նիշ է")]

        public string socNumb { get; set; }




        [Display(Name = "Եկամուտ")]
        public double Income { get; set; }

    }



}