using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CaraOnlineForms
{
    

        public static class GridExtensions
        {
            public const int PAGE_SIZE = 7;
            public const int PAGER_LINKS = 10;

            /// <summary>
            /// generate webgrid columns. sensitive columns will be hidden if the user's access is readonly.
            /// </summary>
            /// <param name="htmlHelper">this is an extension emthod for any htmlHelper</param>
            /// <param name="grid">WebGrid</param>
            /// <param name="openColumns">Columns always shown. Columns will be ordered by the index in dictionary.</param>
            /// <param name="sensitiveColumns">Columns shown if user is allowed to edit/delete/etc. Columns will be ordered by the index in dictionary.</param>
            /// <returns></returns>
            public static WebGridColumn[] GridColumnsCustom(
                this WebGrid grid,
                Dictionary<int, WebGridColumn> openColumns,
                Dictionary<int, WebGridColumn> sensitiveColumns = null
                //Func<object, bool> additionalValidation = null
            )
            {
                Session appSession = new Session();

                if (appSession != null && appSession.User != null && sensitiveColumns != null) //TODO: Setup security check for rendering sensitive columns
                {
                    foreach (KeyValuePair<int, WebGridColumn> pair in sensitiveColumns)
                    { openColumns.Add(pair.Key, pair.Value); } //sensitiveColumns.ElementAt(n).Key, sensitiveColumns.ElementAt(n).Value); 
                }

                return grid.Columns(openColumns.OrderBy(item => item.Key).Select(item => item.Value).ToArray());
            }


            /// <summary>
            /// Create an mvc WebGrid using defaults or supplied params
            /// If autoSortAndPage is false, totalRowCount must be provided for server-side paging to work
            /// </summary>
            /// <param name="htmlHelper"></param>
            /// <param name="sourceData"></param>
            /// <param name="pagerMode"></param>
            /// <param name="ajaxUpdateCallback">jQueryTableStyling required to update theme to match jQuery Theme. If you must call other method, include this call in the other method.
            /// </param>
            /// <param name="rowsPerPage"></param>
            /// <param name="pagerLinksCount"></param>
            /// <param name="defaultSort"></param>
            /// <param name="canPage"></param>
            /// <param name="canSort"></param>
            /// <returns></returns>
            public static WebGrid WebGridCustom(this HtmlHelper htmlHelper,
                                    IEnumerable<object> sourceData,
                                    string ajaxUpdateCallback = "jQueryTableStyling",
                                    string ajaxUpdateContainerId = "grid",
                                    int rowsPerPage = PAGE_SIZE,
                                    string defaultSort = null,
                                    bool canPage = true,
                                    bool canSort = true,
                                    bool autoSortAndPage = true,
                                    int totalRowCount = 0)
            {
                WebGrid grid = null;

                if (autoSortAndPage)
                {   //automatic paging and sorting handled by the grid
                    grid = new WebGrid(sourceData, canPage: canPage, canSort: canSort,
                                                rowsPerPage: rowsPerPage,
                                                ajaxUpdateContainerId: ajaxUpdateContainerId,
                                                ajaxUpdateCallback: ajaxUpdateCallback, //jQuery theme applied
                                                defaultSort: defaultSort);
                }
                else
                {   //paging and sorting handled by the repository
                    grid = new WebGrid(null,
                                                canPage: canPage, canSort: canSort,
                                                rowsPerPage: rowsPerPage,
                                                ajaxUpdateContainerId: ajaxUpdateContainerId,
                                                ajaxUpdateCallback: ajaxUpdateCallback, //jQuery theme applied
                                                defaultSort: defaultSort);
                    grid.Bind(source: sourceData, rowCount: totalRowCount, autoSortAndPage: false);
                }
                return grid;
            }

            /// <summary>
            /// Get the HTML string for a given grid. (render)
            /// </summary>
            /// <param name="grid"></param>
            /// <param name="columns"></param>
            /// <param name="title">grid header</param>
            /// <param name="_htmlAttributes"></param>
            /// <param name="tableStyle"></param>
            /// <param name="headerStyle"></param>
            /// <param name="footerStyle"></param>
            /// <param name="alternatingRowStyle"></param>
            /// <param name="fillEmptyRows"></param>
            /// <param name="emptyRowsCellValue"></param>
            /// <param name="gridWrapperDivID">This identifies the area to fill after an ajax operation</param>
            /// <returns></returns>
            public static MvcHtmlString GetHtmlCustom(this WebGrid grid,
                                            WebGridColumn[] columns,
                                            string title = null,
                                            object _htmlAttributes = null,
                                            string tableStyle = "webgrid",
                                            string headerStyle = "webgrid-header",
                                            string footerStyle = "webgrid-header grid-footer",
                                            string alternatingRowStyle = "webgrid-alternating-rows",
                                            bool fillEmptyRows = false,
                                            string emptyRowsCellValue = null,
                                            string gridWrapperDivID = "grid",
                                            bool scrollable = false,    //if you turn off paging, you probably want to make the grid scrollable instead
                                            WebGridPagerModes pagerMode = WebGridPagerModes.Numeric | WebGridPagerModes.NextPrevious,
                                            int pagerLinksCount = PAGER_LINKS)
            {
                TagBuilder wrapper = new TagBuilder("div");
                wrapper.AddCssClass("webgrid-wrapper");
                TagBuilder titleWrapper = new TagBuilder("div");
                titleWrapper.AddCssClass("webgrid-title");
                titleWrapper.SetInnerText(title);
                wrapper.InnerHtml += titleWrapper.ToString();

                TagBuilder gridWrapper = new TagBuilder("div");
                gridWrapper.Attributes.Add("id", gridWrapperDivID); //this is where ajax ops will rerender returned content

                //make the wrapper div scrollable if paging is turned off
                if (scrollable)
                { gridWrapper.AddCssClass("scrollable"); }

                gridWrapper.InnerHtml += grid.GetHtml(
                    htmlAttributes: (_htmlAttributes ?? new { id = "webGrid" }),
                    tableStyle: tableStyle,
                    headerStyle: headerStyle,
                    footerStyle: footerStyle,
                    alternatingRowStyle: alternatingRowStyle,
                    fillEmptyRows: fillEmptyRows,
                    columns: columns,
                    mode: pagerMode,
                    numericLinksCount: pagerLinksCount).ToString();

                wrapper.InnerHtml += gridWrapper.ToString();

                return MvcHtmlString.Create(wrapper.ToString());
            }

     }
 }

