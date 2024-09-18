using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.ContentManagement
{
    public enum ReviewDateScope
    {
        AllPages = 0,
        MyPages = 1
    }

    public enum ReviewDateStatus
    {
        AllPages = 0,
        PastItsReviewDate = 1,
        NotForOneMonth = 2,
        NotForThreeMonths = 3,
        NotForSixMonths = 4,
        NotModifedForYear = 5
    }

    public enum LinkType
    {
        Page = 0,
        Document = 1,
        Custom = 2,
        Heading = 3,
        NavigationCustomLink = 4
    }

    public enum ApprovalStatus
    {
        Approved, //0
        Denied,   //1
        Pending,  //2
        Draft,    //3
        Scheduled //4
    }
}
