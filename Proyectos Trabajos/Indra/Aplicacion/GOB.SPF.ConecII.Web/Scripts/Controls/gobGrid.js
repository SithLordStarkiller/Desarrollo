/**
 * Modulo       : GobGrid.js
 * Descripcion	: Definición de metodos para control rico Grid
 * Autor	    : Oscar Maya
 * Creado       : Jueves 28, Febrero 2013
 * Proyecto	    : Conec
 * Observaciones: 
 */

/**
 * Configura el control y asigna comportamiento
 * @param {columns} columns que contendra el grid que contiene el grid.
 */
gob.Controls.Grid.prototype.init = function(columns){

    var self = this;
    var doc = document;
    var cons = gob.Constants.cellType;

    this.content = doc.createElement('div');
    this.content.className = 'grid-content';

    var createTable = function (cssClass) {
        var table = doc.createElement('table');
        table.className = cssClass;
        return table;
    };
    this.pagerNavButtonInactiveClass = "active";
    this.pages = this.defaultData.pager.pages;
    this.pageSize = this.defaultData.pager.pageSize;
    this.pagerFormat = " {first} {prev} {pages} {next} {last} &nbsp;&nbsp; {pageIndex} de {pageCount}";
    this.currentPage = this.defaultData.pager.currentPage;
    this.showPlusButton = this.defaultData.showPlusButton;
    this.theadLoaded = false;
    this.theadFooter = false;
    this.columns = columns;
    var contentHead = doc.createElement('thead');
    var contentBody = doc.createElement('tbody');
    var contentFooter = doc.createElement('tfoot');
    this.Paging();

    this.tcontent = createTable('table table-responsive');

    var theadColumns = function (theadLoaded, contentHead) {
        if (!theadLoaded) {
            var trHead = doc.createElement('tr');            
            for (var indexCol = 0, lenCols = columns.length; indexCol < lenCols; indexCol++) {
                var th = doc.createElement('th');                
                th.className = columns[indexCol].css;
                if (self.showPlusButton && indexCol === lenCols - 1) {
                    var button = doc.createElement('button');
                    button.className = "btn btn-default btn-xs";
                    var spanBtn = doc.createElement("span");
                    spanBtn.className = "glyphicon glyphicon-plus New ";
                    button.appendChild(spanBtn);
                    th.appendChild(button);
                    $(button).click(function (e) {
                        self.execActionButton(this, e);
                    });
                } else {
                    th.innerHTML = columns[indexCol].text;
                }

                trHead.appendChild(th);

            }
            self.theadLoaded = true;
            return trHead;
        }
    };

    this.GetfooterPaging = function () {
        var tfooter = self.tcontent.childNodes[2];
        var ul = tfooter.find("td ul.pagination");
    }

    var getfooter = function (columns) {
        var tr = doc.createElement("tr");
        var td = doc.createElement("td");
        self.footer = td;
        td.setAttribute("colspan", columns.length);
        td.setAttribute("align", "center");

        if (self.defaultData.pager.show) {
            createPager()            
            td.appendChild(createPager());
        }

        tr.appendChild(td);
        return tr;
    };

    contentHead.appendChild(theadColumns(this.theadLoaded, contentHead));    
    contentFooter.appendChild(getfooter(this.columns));
    this.tcontent.appendChild(contentHead);
    this.tcontent.appendChild(contentBody);
    this.tcontent.appendChild(contentFooter);
    this.grid.className = "grid-container";
    this.grid.appendChild(this.tcontent);
    
    

};


gob.Controls.Grid.prototype.Paging = function () {
    var self = this;
    createPager = function () {
        var $result;

        $("ul.pagination").empty();
        //if ($.isFunction(refreshPager)) {
        //    $result = $(refreshPager({
        //        pageIndex: self.currentPage,
        //        pageCount: self.pages
        //    }));
        //} else {
            $result = $("<ul>").addClass("pagination").append(this.createPagerByFormat());
        //}

        $result.addClass(this.pagerClass);

        return $result[0];
    };

    createPagerByFormat = function () {
        var FIRST_PAGE_PLACEHOLDER = "{first}",
        PAGES_PLACEHOLDER = "{pages}",
        PREV_PAGE_PLACEHOLDER = "{prev}",
        NEXT_PAGE_PLACEHOLDER = "{next}",
        LAST_PAGE_PLACEHOLDER = "{last}",
        PAGE_INDEX_PLACEHOLDER = "{pageIndex}",
        PAGE_COUNT_PLACEHOLDER = "{pageCount}",
        ITEM_COUNT_PLACEHOLDER = "{itemCount}";
        this.pageIndex = self.currentPage;
        var pageCount = self.pages,
            itemCount = self.pageSize,
            pagerParts = self.pagerFormat.split(" ");

        return $.map(pagerParts, $.proxy(function(pagerPart) {
            var result = pagerPart;

            if(pagerPart === PAGES_PLACEHOLDER) {
                result = createPages();
            } else if(pagerPart === FIRST_PAGE_PLACEHOLDER) {
                result = createPagerNavButton("|<", 1, pageIndex > 1);
            } else if(pagerPart === PREV_PAGE_PLACEHOLDER) {
                result = createPagerNavButton("<", pageIndex - 1, pageIndex > 1);
            } else if(pagerPart === NEXT_PAGE_PLACEHOLDER) {
                result = createPagerNavButton(">", pageIndex + 1, pageIndex < pageCount);
            } else if(pagerPart === LAST_PAGE_PLACEHOLDER) {
                result = createPagerNavButton(">|", pageCount, pageIndex < pageCount);
            } else if(pagerPart === PAGE_INDEX_PLACEHOLDER) {
                result = pageIndex;
            } else if(pagerPart === PAGE_COUNT_PLACEHOLDER) {
                result = pageCount;
            } else if(pagerPart === ITEM_COUNT_PLACEHOLDER) {
                result = itemCount;
            }

            return $.isArray(result) ? result.concat([" "]) : [result, " "];
        }, this));
    };

    createPages= function() {
        var pageCount = self.pages,
            pageButtonCount = self.pages,
            firstDisplayingPage = 1,
            pages = [];

        if(firstDisplayingPage > 1) {
            pages.push(createPagerPageNavButton("...", this.showPrevPages));
        }

        for(var i = 0, pageNumber = firstDisplayingPage; i < pageButtonCount && pageNumber <= pageCount; i++, pageNumber++) {
            pages.push(pageNumber === this.pageIndex
                ? createPagerCurrentPage(pageNumber)
                : createPagerPage(pageNumber));
        }

        if((firstDisplayingPage + pageButtonCount - 1) < pageCount) {
            pages.push(createPagerPageNavButton(this.pageNavigatorNextText, this.showNextPages));
        }

        return pages;
    };

    createPagerNavButton = function(text, pageIndex, isActive) {
        return createPagerButton(text, this.pagerNavButtonClass + (isActive ? "" : " " + self.pagerNavButtonInactiveClass),$.noop);
    };

    createPagerPageNavButton = function(text, handler) {
        return createPagerButton(text, this.pagerNavButtonClass, handler);
    };

    createPagerPage = function(pageIndex) {
        return createPagerButton(pageIndex, $.noop());
    };

    createPagerButton = function(text, css, handler) {
        var $link = $("<span>").addClass(css)
            .html(text).click(function (evt) {
                self.fire({ type: 'onPage', currentPage: text });
            });
        
        return $("<li>").addClass("page-item").append($link);
    };

    createPagerCurrentPage = function (text) {

        var $link = $("<span>").addClass("page-link")
           .html(text);

        return $("<li>").addClass("page-item").append($link).addClass("active");
    };

    
};
gob.Controls.Grid.prototype.loadRows = function (rows) {
    var self = this;
    var doc = document;
    var cons = gob.Constants.cellType;
    var thead = this.tcontent.childNodes[0];
    var tbody = this.tcontent.childNodes[1];
    var tfooter = this.tcontent.childNodes[2];

    var formatCurrency = function (n) {
        return '$ ' + n.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
    };
    var filterCurrency = function (e, decimalPlaces) {
        var code = e.which;
        var key = String.fromCharCode(code);
        var field = e.target;
        var isControlKey = (code === 0 || code === 8 || code === 37 || code === 39 || code === 46);
        var valid = (key === '.') ? field.value.indexOf('.') === -1 :
					(!isControlKey) ? /[0-9]|\./.test(key) : !/[°|!\"#$%&\/()\'=?¿¡\[\]\*]/.test(key);
        var valueParts = field.value.split('.');
        var len = (field.setSelectionRange) ? (field.selectionEnd - field.selectionStart) : document.selection.createRange().text.length;

        if (valid && !isControlKey && valueParts.length > 1 && len === 0) {
            valid = valueParts[1].length < decimalPlaces;
        }

        return valid;
    };    

    
    if (!rows || rows.length === 0) {
        $(tbody).find("tr").remove();
        $(tbody).addClass('no-data');
        this.fire({ type: 'onLoadComplete', rows: 0 });        
        return this;
    }

    $(tbody).removeClass('no-data');
    var increment = 1;
    for (var indexRow = 0, len = rows.length; indexRow < len; indexRow++) {
        var key = '';
        var isDetailHandler = false;
        var dataRow = rows[indexRow];
        var trow = doc.createElement('tr');
        var $trow = $(trow);
        $trow.data('dataRow', dataRow)
             .click(function (e) { self.selectRow(this, e); });

        for (var indexCol = 0, lenCols = this.columns.length; indexCol < lenCols; indexCol++) {
            //var isHidden = $(this.theader.rows[0].cells[indexCol]).hasClass('grid-hidden-col');
            var cell = doc.createElement('td');                        
            var column = $.extend(true, {}, this.columnDefault, this.columns[indexCol]);
            cell.className = column.css;
            var value = dataRow[column.field];
            var cellValue = (column.field && column.field.length > 0) ? value : '';
            cellValue = column.iskey ? self.currentPage===1 ? increment++ : (self.pageSize * (self.currentPage-1)) + increment++ : (cellValue === undefined || cellValue === null) ? '' : cellValue;

            switch (true) {
                case (typeof cellValue === 'string'):
                    //cellValue = cellValue.replace(/</g, '&lt;').replace(/>/g, '&gt;')

                    //if (cellValue.indexOf('/Date(') !== -1) {
                        //var date = eval('new ' + cellValue.replace(/\//g, ''));
                        //cellValue = date.format(column.formatDate);                        
                    //}
                    break;

                case (typeof cellValue === 'boolean' && column.cellType.type === cons.NORMAL):
                    cellValue = (cellValue) ? 'SI' : 'NO';
                    break;

                case (typeof cellValue === 'number'):
                    cellValue = (column.currency) ? formatCurrency(cellValue) : cellValue;
                    break;

                case (typeof cellValue === 'object' && typeof cellValue.getDate === 'function'):
                    cellValue = cellValue.format(column.formatDate);
                    break;
            }

            if (column.columnType === gob.Constants.columnType.NUMBER) {
                $(cell).addClass('grid-numeric-cell');
            }

            if (column.fieldValueReplacement && $.isFunction(column.fieldValueReplacement)) {
                cellValue = column.fieldValueReplacement.call(this, dataRow, cellValue);
            }

            cell.innerHTML = cellValue;

            switch (column.cellType.type) {
                case cons.NORMAL:
                    if (this.defaultData.detail.field !== '' && !isDetailHandler) {
                        var rowHandler = doc.createElement('span');
                        rowHandler.className = 'grid-cell-control grid-handler-detail';
                        rowHandler.setAttribute('title', 'Mostrar detalle');
                        $(rowHandler).click(function () {
                            var $this = $(this);
                            var row = this.parentNode.parentNode;

                            $this.toggleClass('grid-detail-expanded');

                            if ($this.hasClass('grid-detail-expanded')) {
                                $this.attr('title', 'Ocultar detalle');
                            } else {
                                $this.attr('title', 'Mostrar detalle');
                            }

                            $(row).next('[rel="' + row.id + '"]').toggle('fast', function () {
                                
                            });
                        });
                        $(cell.firstChild).before(rowHandler);
                        isDetailHandler = true;
                    }
                    break;

                case cons.CHECKBOX:
                    var check = doc.createElement('input');
                    check.className = 'cell-check';
                    check.setAttribute('type', 'checkbox');
                    check.checked = cellValue;
                    $(check).click(function (event) { self.checkCell(this, event); });
                    cell.innerHTML = '';
                    cell.appendChild(check);
                    break;

                case cons.RADIO:
                    var radio = doc.createElement('input');
                    radio.className = 'cell-radio';
                    radio.setAttribute('type', 'radio');
                    radio.setAttribute('name', 'GridRadio');
                    radio.setAttribute('value', cellValue);
                    $(radio).click(function (event) { self.checkRadio(this, event); });
                    cell.innerHTML = '';
                    cell.appendChild(radio);
                    break;

                case cons.ACTION:
                    var cssClass = (column.cellType.cssClass &&
                        typeof cellValue === 'boolean') ? (cellValue) ? column.cellType.cssClass.active
                        : column.cellType.cssClass.inactive
                        : (column.cellType.cssClass) ? column.cellType.cssClass.normal : '';
                    var tooltip = ($.isFunction(column.cellType.tooltip)) ? column.cellType.tooltip.call(this, cellValue) : column.cellType.tooltip;
                    var action = doc.createElement('span');
                    action.className = 'grid-action';
                    action.setAttribute('title', tooltip);
                    action.className += (' ' + cssClass);
                    $(action).click(function (event) { self.execAction(this, event); });
                    cell.innerHTML = '';
                    cell.appendChild(action);

                    break;

                case cons.BUTTON:
                    cssClass = (column.cellType.cssClass &&
                        typeof cellValue === 'boolean') ? (cellValue) ? column.cellType.cssClass.active
                        : column.cellType.cssClass.inactive
                        : (column.cellType.cssClass) ? column.cellType.cssClass.normal : '';
                    var button = doc.createElement('button');                    
                    button.className = "btn btn-default btn-xs";
                    button.setAttribute('value', (column.cellType.text) ? column.cellType.text : '');
                    var spanBtn = doc.createElement("span");
                    switch (cssClass) {
                        case 'Edit':                            
                            spanBtn.className = "glyphicon glyphicon-pencil edit ";
                            button.setAttribute('data-cmd', cssClass);
                            button.appendChild(spanBtn);
                            break;
                        case 'View':
                            spanBtn.className = "glyphicon glyphicon-eye-open view ";
                            button.setAttribute('data-cmd', cssClass);
                            button.appendChild(spanBtn);
                            break;
                        case 'Active':
                        case 'Inactive':
                            if (cellValue)
                                $(button).removeClass("btn-default").addClass("btn-primary")
                            else
                                $(button).removeClass("btn-default").addClass("btn-danger")
                            spanBtn.className = "glyphicon glyphicon-off delete ";
                            button.setAttribute('data-cmd', cssClass);
                            button.appendChild(spanBtn);
                            break;
                    }
                    
                    $(button).click(function (e) {                        
                        self.execActionButton(this, e);
                    });
                    cell.innerHTML = '';
                    cell.appendChild(button);

                    break;

                case cons.COMBO:
                    var cbo = doc.createElement('select');
                    cbo.className = 'grid-data-combo';
                    $(cbo).data('cboData', column);

                    $.each(column.cellType.combo.items, function (index, item) {
                        var value = eval('this.' + column.cellType.combo.fieldValue);
                        var text = eval('this.' + column.cellType.combo.fieldText);
                        var enabled = eval('this.' + column.cellType.combo.fieldEnable);

                        var element = document.createElement('option');
                        element.value = value;
                        element.innerHTML = text;
                        element.setAttribute('title', text);
                        cbo.appendChild(element);

                        if (index === 0) {
                            eval('dataRow.' + column.cellType.combo.fieldSet + ' = "' + value + '"');
                        }

                        if (text === cellValue || value === cellValue) {
                            element.setAttribute('selected', 'selected');
                            eval('dataRow.' + column.cellType.combo.fieldSet + ' = "' + value + '"');
                        }

                        if (typeof enabled === 'boolean') {
                            element.disabled = !enabled;
                        }
                    });

                    $(cbo).change(function () {
                        var option = this.options[this.selectedIndex];
                        var value = option.value;
                        var data = $(this.parentNode.parentNode).data('dataRow');
                        var column = $(this).data('cboData');

                        eval('data.' + column.cellType.combo.fieldSet + ' = "' + value + '"');
                    });

                    if (!column.cellType.initStateEnabled) {
                        cbo.setAttribute('disabled', 'disabled');
                    }

                    cell.innerHTML = '';
                    cell.appendChild(cbo);
                    break;

                case cons.TEXTBOX:
                    var txt = doc.createElement('input');
                    txt.setAttribute('type', 'text');
                    txt.className = 'grid-text-field';
                    txt.value = cellValue;
                    $(txt).data('txtField', { column: column, index: indexCol })
                          .keyup(function () {
                              var cell = this.parentNode;
                              var row = cell.parentNode;
                              var col = ($.browser.msie && parseFloat($.browser.version) < 8.0) ? $(row.cells).index(cell) : cell.cellIndex;
                              self.fire({ type: 'onKeyUp', textbox: this, value: this.value.replace(/[,$\s]/g, ''), dataRow: $(row).data('dataRow'), cell: cell, row: row, col: col });
                          })
                          .bind('blur', function (e) {
                              var txtData = $(this).data('txtField');
                              var fieldName = txtData.column.field;
                              var data = $(this.parentNode.parentNode).data('dataRow');
                              var valueField = this.value;
                              var element = undefined;

                              if (txtData.column.currency) {
                                  this.value = (this.value !== '') ? this.value : '0';

                                  valueField = parseFloat(this.value.replace(/[,$\s]/g, ''));
                                  eval('data.' + fieldName + ' = ' + valueField);
                                  this.value = formatCurrency(valueField);
                              } else {
                                  eval('data.' + fieldName + ' = "' + this.value + '"');
                              }

                              if ($.browser.msie) element = document.activeElement;
                              if ($.browser.mozilla) element = e.originalEvent.explicitOriginalTarget;
                              if ($.browser.webkit) element = e.relatedTarget;

                              var eventArgs = { type: 'onEditedField', value: valueField, dataRow: data, textbox: this, column: txtData.index, cancel: false, element: element }
                              self.fire(eventArgs);

                              if (eventArgs.cancel) {
                                  e.preventDefault();
                                  e.stopPropagation();

                                  return false;
                              }
                          });

                    if (column.columnType === gob.Constants.columnType.NUMBER) {
                        $(txt).addClass('grid-numeric-cell')
                            .keypress(function (e) { return filterCurrency(e, 2); })
                            .focus(function (e) { this.value = this.value.replace(/[,$\s]/g, ''); })
                            .click(function (e) {
                                  var cell = this.parentNode;
                                  var row = cell.parentNode;
                                  self.fire({ type: 'onClick', textbox: this, value: this.value.replace(/[,$\s]/g, ''), dataRow: $(row).data('dataRow'), cell: cell, row: row, col: cell.cellIndex });
                              });
                    }

                    if (!column.cellType.initStateEnabled) {
                        txt.setAttribute('disabled', 'disabled');
                    }

                    cell.innerHTML = '';
                    cell.appendChild(txt);
                    break;

                case cons.LINK:
                    var link = doc.createElement('a');
                    link.setAttribute('href', column.cellType.url + cellValue);
                    link.innerHTML = cellValue;
                    cell.innerHTML = '';
                    cell.appendChild(link);
            }

            if (column.tooltip) {
                cell.setAttribute('title', cellValue);
            }

            //if (!column.visible || isHidden) {
            //    $(cell).addClass('grid-hidden-col');
            //}

            trow.appendChild(cell);
        }

        if (key.length > 0) trow.id = key;

        tbody.appendChild(trow);

        this.fire({ type: 'onRowLoaded', row: trow, dataRow: dataRow });

        if (this.defaultData.detail.field !== '') {
            this.loadDetail(tbody, doc, trow, dataRow);
        }
    }

    if (self.defaultData.pager.show) {
        $(self.footer).find("ul").remove();
        self.footer.appendChild(createPager());
    }
    //this.Paging();
    //createPager();

    this.selectedRow = -1;
    this.fire({ type: 'onLoadComplete', rows: rows.length });
};

gob.Controls.Grid.prototype.execActionButton = function (button, event) {
    var row = button.parentNode.parentNode;
    var column = $.extend(true, {}, this.columnDefault, this.columns[button.parentNode.cellIndex]);

    this.fire({ type: 'onButtonClick', row: row.rowIndex, column: button.parentNode.cellIndex, dataRow: $(row).data('dataRow'), key: row.id, event: this.showPlusButton && row.rowIndex === 0 ? "New" : button.getAttribute("data-cmd") });

    if (event) {
        event.stopPropagation();
    }
};

gob.Controls.Grid.prototype.selectRow = function (htmlRow, event) {
    if (this.defaultData.showSelectedRow) {
        $(this.tcontent.rows[this.selectedRow]).removeClass('row-selected');
        $(htmlRow).addClass('row-selected');
    }

    this.selectedRow = htmlRow.rowIndex;
    this.fire({ type: 'onselectRow', row: htmlRow, dataRow: $(htmlRow).data('dataRow'), index: this.selectRow, key: htmlRow.id });

    if (event) {
        event.stopPropagation();
    }
};

gob.Controls.Grid.prototype.reload = function (rows) {
    var headRowLayout = this.tcontent[0];

    this.cacheRows = undefined;
    var tbody = this.tcontent.childNodes[1];
    $(tbody).find("tr").remove();
    $(rows).each(function (index) {
        if (index > 0) {
            $(this).remove();
        }
    });

    //$(this.theader).find('td :checked')
	//			   .each(function (index, item) {
	//			       item.checked = false;
	//			   });

    this.loadRows(rows);
}
