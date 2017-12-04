using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    /// <summary>
    /// Interface for Viewmodels of views that support pagging.
    /// </summary>
    public interface IPagedView
    {
        #region Members

        /// <summary>
        /// Gets a value indicating whether this instance has previous page.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has previous page; otherwise, <c>false</c>.
        /// </value>
        bool HasPreviousPage { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has next page.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has next page; otherwise, <c>false</c>.
        /// </value>
        bool HasNextPage { get; }

        /// <summary>
        /// Gets the previous page number.
        /// </summary>
        /// <value> The previous page number.</value>
        int PreviousPage { get; }

        /// <summary>
        /// Gets the next page number.
        /// </summary>
        /// <value>The next page number.</value>
        int NextPage { get; }

        /// <summary>
        /// Gets the page items.
        /// </summary>
        /// <value>The page items.</value>
        [UIHint("IEnumerable")]
        IEnumerable PageItems { get; }

        IPageBase Filtros { get; }


        #endregion
    }
}
