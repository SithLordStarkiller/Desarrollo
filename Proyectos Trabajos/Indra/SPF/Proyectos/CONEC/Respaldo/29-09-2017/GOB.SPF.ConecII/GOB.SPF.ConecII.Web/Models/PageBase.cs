using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class PageBase : IPageBase
    {
        /// <summary>
        /// Current page.
        /// </summary>
        protected int mPage;
        /// <summary>
        /// Size of the current page.
        /// </summary>
        protected int mPageSize;
        /// <summary>
        /// Total of the items.
        /// </summary>
        protected int mTotalItems;

        public int? ItemId { get; set; } //Llave

        public int PageCurrent
        {
            get
            {
                if (mPage.Equals(0))
                    mPage = 1;

                return mPage;
            }
            set { mPage = value; }
        }

        public int PageNumber { get; set; }

        public int PageSize
        {
            get
            {
                if (mPageSize.Equals(0))
                    mPageSize = 5;
                return mPageSize;
            }
            set { mPageSize = value; }
        }

        public int PageTotalItems
        {
            get { return mTotalItems; }
            set { mTotalItems = value; }
        }

    }
}
