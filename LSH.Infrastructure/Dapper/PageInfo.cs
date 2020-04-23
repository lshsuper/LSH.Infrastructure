using System;
using System.Collections.Generic;
using System.Text;

namespace LSH.Infrastructure.Dapper
{
    public class PageInfo<T>where T: class,new()
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int DataCount { get; set; }


        #region +Extension

        public int PageCount
        {
            get
            {
                //当每页条数设置的小于0时，这里赋值默认值
                if (PageSize <= 0)
                    PageSize = 20;

               return Convert.ToInt32(Math.Ceiling(DataCount/(double)PageSize));
            }
        }
        public IEnumerable<T> Data { get; set; }
        #endregion

    }
}
