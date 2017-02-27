using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace KMOL.Web.Data
{
    public class KMOLContextFactory
    {
        public KMOLContext Create(bool isHomeLinks,DateTime usedDate, ref DateTime realUsedDate)
        {
            return new KMOLContext(isHomeLinks,usedDate,ref realUsedDate);
        }
        public KMOLContext Create(bool isHomeLinks, DateTime usedDate)
        {
            return new KMOLContext(isHomeLinks, usedDate);
        }
    }
}