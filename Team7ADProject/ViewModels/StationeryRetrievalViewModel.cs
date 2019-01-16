﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Team7ADProject.Entities;

//Author: Sam Jing Wen
namespace Team7ADProject.ViewModels
{
    public class StationeryRetrievalViewModel
    {
        [StringLength(4)]
        public string ItemId { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [StringLength(25)]
        public string UnitOfMeasure { get; set; }

        public int Quantity { get; set; }

        [Required]
        [StringLength(50)]
        public string Location { get; set; }

        [StringLength(10)]
        public string RequestId { get; set; }
    }

    public class CompiledRequestViewModel
    {
        List<StationeryRetrievalViewModel> RetrievalList
        {
            get
            {
                LogicDB context = new LogicDB();
                List<StationeryRetrievalViewModel> newList = new List<StationeryRetrievalViewModel>();
                var query = from x in context.StationeryRequest
                            where x.Status.Equals("Pending Disbursement")
                            join y in context.TransactionDetail 
                            on x.RequestId equals y.TransactionRef
                            select new StationeryRetrievalViewModel
                            {
                                ItemId = y.ItemId,
                                Category = y.Stationery.Category,
                                Description = y.Stationery.Description,
                                UnitOfMeasure = y.Stationery.UnitOfMeasure,
                                Location = y.Stationery.Location,
                                RequestId = x.RequestId
                            };





                //foreach (var request in query)
                //{
                //    var trxList = request.TransactionDetail;
                //    foreach (var trx in trxList)
                //    {
                //        if (newList.Exists(x => x.ItemId == trx.ItemId))
                //        {
                //            var srvm = newList.Find(x => x.ItemId == trx.ItemId);
                //            srvm.Quantity += trx.Quantity;
                //        }

                //    }
                //}
                return newList;
            }

        }

    }



}