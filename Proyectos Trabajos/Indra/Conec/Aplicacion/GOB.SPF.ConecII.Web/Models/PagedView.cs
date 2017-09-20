using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class PagedView : IPagedView
    {
        protected IEnumerable mEntity;
        protected IPageBase mFilter;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="entity">The customers.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        public PagedView(IEnumerable entity, IPageBase filter)
        {

            mEntity = entity;
            mFilter = filter;
        }

        #endregion

        #region Members

        /// <summary>
        /// Gets a value indicating whether this instance has previous page.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has previous page; otherwise, <c>false</c>.
        /// </value>
        public bool HasPreviousPage
        {
            get { return mFilter.PageCurrent > 1; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has next page.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has next page; otherwise, <c>false</c>.
        /// </value>
        public bool HasNextPage
        {
            get { return mFilter.PageTotalItems > mFilter.PageCurrent * mFilter.PageSize; }
        }

        /// <summary>
        /// Gets the previous page number.
        /// </summary>
        /// <value>The previous page number.</value>
        public int PreviousPage
        {
            get { return mFilter.PageCurrent - 1; }
        }

        /// <summary>
        /// Gets the next page number.
        /// </summary>
        /// <value>The next page number.</value>
        public int NextPage
        {
            get { return mFilter.PageCurrent + 1; }
        }

        /// <summary>
        /// Gets the page items.
        /// </summary>
        /// <value>The page items.</value>
        public IEnumerable PageItems
        {
            get { return mEntity; }
        }

        public IPageBase Filtros
        {
            get { return mFilter; }
        }


        #endregion

    }
}
