﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team7ADProjectApi.Entities;
using Team7ADProjectApi.ViewModels;

namespace Team7ADProjectApi
{
    public class GlobalClass
    {
        LogicDB context = new LogicDB();

        #region Author: Sam Jing Wen

        public List<BriefDepartment> ListDepartment(string id)
        {
            var query = from x in context.Department
                        join y in context.AspNetUsers
                        on x.DepartmentRepId equals y.Id
                        where x.DepartmentId == id
                        select new BriefDepartment
                        {
                            DepartmentId = x.DepartmentId,
                            DepartmentName = x.DepartmentName,
                            DepartmentRepName = y.EmployeeName,
                            DepartmentRepId = x.DepartmentRepId

                        };
            return query.ToList();
        }

        public List<RequestItems> ListRequestByItem()
        {
            var query = (from x in context.RequestByItemView select new RequestItems
                            {
                               ItemId = x.ItemId,
                               Description = x.Description
                            }).Distinct().ToList();
            return query;
        }

        #endregion



        #region Author : Kay Thi Swe Tun
        public List<DepEmp> ListEmp(string id)
        {
            var depid = getDepId(id);
            var query = from y in context.AspNetUsers
                        where y.DepartmentId == depid
                        select new DepEmp
                        {
                          EName=y.EmployeeName,
                          Empid=y.Id,
                          Email=y.Email,
                          phone=y.PhoneNumber
                         };
            return query.ToList();
        }

        public void assignDepRep(BriefDepartment e)
        {
            
            //Retrieve department head
            //string depHeadId;
            // var user = database.AspNetUsers.Where(x => x.Id == depHeadId).FirstOrDefault();
            //Retrieve department
            var dept = context.Department.Where(x => x.DepartmentId == e.DepartmentId).FirstOrDefault();

            //Change department rep
            string oldEmpRepId = dept.DepartmentRepId;
            //string userId = model.UserId;
            dept.DepartmentRepId = e.DepartmentRepId;
            context.SaveChanges();
            //Change previous Department Rep to employee
            //manager.RemoveFromRole(oldEmpRepId, "Department Representative");
           // manager.AddToRole(oldEmpRepId, "Employee");
            //Assign new employee to Department Rep
           // manager.RemoveFromRole(userId, "Employee");
            //t6manager.AddToRole(userId, "Department Representative");
        }

        public string getDepId(string eid)
        {
            return context.AspNetUsers.Where(x => x.Id == eid).Select(x=>x.DepartmentId).First();
            

        }
        public BriefDepartment DepInfo(string id)
        {
            var depid = getDepId(id);
        
            var query = from x in context.Department
                        join y in context.AspNetUsers
                        on x.DepartmentRepId equals y.Id
                        where x.DepartmentId == depid
                        select new BriefDepartment
                        {
                            DepartmentId = x.DepartmentId,
                            DepartmentName = x.DepartmentName,
                            DepartmentRepName = y.EmployeeName,
                            DepartmentRepId = x.DepartmentRepId

                        };
            return query.First();
        }
       

    #endregion

}
}