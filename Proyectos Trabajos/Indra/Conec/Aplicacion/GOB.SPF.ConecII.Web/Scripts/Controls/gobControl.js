/**
 * Metodo para copiar el prototipo del un objeto
 * @param {obj} obj del cual copiar el prototipo
 * @returns {Function} El valor de la funcion.
 */
function object(obj) {
    function f() {
    }
    f.prototype = obj;
    return new f();
}

/** Metodo para herdar el prototipo de un objeto en otro 
 * @param {subtype} subtype objeto que hereda
 * @param {object} superType objeto base o superclase
 */
function inheritPrototype(subtype, superType) {
    var prototype = object(superType.prototype);

    prototype.constructor = subtype;
    subtype.prototype = prototype;
}

// Objeto para disparar eventos desde los controles
function EventObject() {
    this.listeners = {};
}

/** Definición de clase base, implementa patron observer
 * @method <addListener>	Agrega un escuchador
 * @method <removeListener>	Elimina un escuchador
 * @method <fire>			dispara un evento
 */
EventObject.prototype = {
    constructor: EventObject,
    /** Agrega una función a la lsita de oyentes en espera de procesar un evento
	 * @param {string} type Nombre del evento a escuchar
	 * @param {object} listener Función escuchadora
	 */
    addListener: function (type, listener) {
        if (typeof this.listeners[type] === "undefined") {
            this.listeners[type] = [];
        }
        this.listeners[type].push(listener);
    },
    removeListener: function (type, listener) {
        if (this.listeners[type] instanceof Array) {
            var listeners = this.listeners[type];
            for (var item = 0, len = listeners.length; item <= len; item++) {
                if (listeners[item] === listener) {
                    break;
                }
            }
            listeners.splice(item, 1);
        }
    },

    /** Dispara un evento
     * @param {event} event con los datos del evento
     */
    fire: function (event) {
        if (!event.target) {
            event.target = this;
        }
        if (this.listeners[event.type] instanceof Array) {
            var listeners = this.listeners[event.type];
            for (var item = 0, len = listeners.length; item < len; item++) {
                listeners[item](event);
            }
        }
    }
}

var gob = {
    AppService: {
        Constants: {
            dateFormat: {DMY: 0, MDY: 1, YMD: 2},
            input: {LTR: 0, RTL: 1}
        },
        Application: function(){
            var cons = gob.AppService.Constants;
			
            this.fields = [];
            this.fieldDefault = {
                id: '',
                groupValidation: '',
                mask: {format: '', input: cons.input.RTL},
                required: {validate: false, message: ''},
                compare: { compare: false, compareWith: undefined, message: ''},
                date: {validate: false, message: '', format: cons.dateFormat.DMY},
                email: {validate: false, message: ''},
                custom: {validate: false, method: undefined, message: ''}
            };			
        }
    }, Constants: {
        columnType: {TEXT: 0, NUMBER: 2, DATE: 3},
        cellType: {NORMAL: 0, CHECKBOX: 1, RADIO: 2, TEXTBOX: 3, COMBO: 4, BUTTON: 5, IMAGE: 6, ACTION: 7, LINK: 8},
        buttonType: {CUSTOM: 0, OK: 1, CANCEL: 2},
        windowIcon: {NONE: 0, OK: 1, WARNING: 2, ERROR: 3, QUESTION: 4}
    },
    Controls: {

        /**
		 * Definición de control cuadricula
		 * @param {element} element si ok
		 * @param {options} options si ok
		 */
        Grid: function (element, options) {
            if (typeof element !== 'object') {
                this.grid = document.getElementById(element);
            } else {
                this.grid = element;
            }

            this.defaultData = $.extend(true, {
                height: 0,
                maxHeight: 0,
                striped: true,
                showSelectedRow: false,
                showPlusButton:false,
                pager: {
                    show: false,
                    onServer: false,
                    sizes: [10, 25, 50, 100, 150, 200],
                    pageSize: 10,
                    currentPage: 0,
                    pages: 0
                },
                detail: {
                    field: '',
                    columns: []
                }
            }, options);

            this.columnDefault = {
                iskey: false,
                isHierarchy:false,
                text: '',
                width: 0,
                field: '',
                fieldValueReplacement: undefined,
                tooltip: false,
                visible: true,
                currency: false,
                formatDate: 'default',
                columnType: gob.Constants.columnType.TEXT,
                colSpan: 0,
                css:'',
                sort: {
                    sortable: false,
                    sorted: 0
                },
                cellType: {
                    type: gob.Constants.cellType.NORMAL,
                    combo: {
                        items: [],
                        fieldValue: '',
                        fieldText: '',
                        fieldSet: '',
                        fieldEnable: undefined
                    },
                    text: '',
                    cmd: '',
                    url: '',
                    initStateEnabled: true,
                    mask: { format: '', input: '' },
                    cssClass: {
                        active: '',
                        inactive: '',
                        normal: ''
                    },
                    tooltip: ''
                }
            };

            EventObject.call(this);
        }
    }
};

(function () {
    var ExtenderMethods = {
        trim: function () {
            return this.replace(/^\s\s*/, '').replace(/^\s\s*/, '');
        },
        padLeft: function (length, character) {
            return new Array(length - this.length + 1).join(character || ' ') + this;
        },
        padRight: function (length, character) {
            return this + new Array(length - this.length + 1).join(character || ' ');
        },
        format: function (values) {   // use: 'Prototype is: {key1} and {key2}'.format({'key1': 'Powerfull', 'key2': 'Simple'});
            var regex = /\{([\w-]+)(?:\:([\w\.]*)(?:\((.*?)\))?)?\}/g;

            var getValue = function (key) {
                if (values === null || typeof values === 'undefined') return null;

                var value = values[key];
                var type = typeof value;

                return type === 'string' || type === 'number' ? value : null;
            };

            return this.replace(regex, function (match) {
                var key = match.substr(1, match.length - 2);
                var value = getValue(key);

                return value !== null ? value : match;
            });
        }
    };

    for (var method in ExtenderMethods) {
        String.prototype[method] = String.prototype[method] || ExtenderMethods[method];
    }
})();

inheritPrototype(gob.Controls.Grid, EventObject);

function getAbsoluteTop(object) { return getOffsetRect(object).top; }
function getAbsoluteLeft(object) { return getOffsetRect(object).left; }
function getOffset(object) { return getOffsetRect(object); }

function getOffsetRect(object) {
    var box = object.getBoundingClientRect();
    var body = document.body;
    var element = document.documentElement;
    var scrollTop = window.pageYOffset || element.scrollTop || body.scrollTop;
    var scrollLeft = window.pageXOffset || element.scrollLeft || body.scrollLeft;
    var clientTop = element.clientTop || body.clientTop || 0;
    var clientLeft = element.clientLeft || body.clientLeft || 0;
    var top = box.top + scrollTop - clientTop;
    var left = box.left + scrollLeft - clientLeft;

    return { top: Math.round(top), left: Math.round(left) };
}