gob.Controls.Treeview.prototype.init = function (node) {
    var self = this;
    var data = this.defaultData;
    if (!Array.isArray(node)) {
        throw "El objeto ingresado no es un arraglo.";
    }

    var level = 0;
    var addNode = function (node, level) {
        if (level === undefined) {
            level = 0;
        }
        var ul = document.createElement("ul");
        for (var i = 0; i < node.length; i++) {
            var label = document.createElement("label");
            var check = document.createElement("input");
            check.type = "checkbox";
            var span = document.createElement("span");
            span.className = "cr";
            label.innerHTML = node[i][data.text];
            $(check).click(function (evt) {
                var self = this;
                var parentli = this.parentNode.parentNode.parentNode.parentNode;
                var childs = $(self.parentNode.parentNode).find("ul");
                if (childs.hasChildNodes) {
                    var children = self.children;
                    var childrenInput = children.find("input[type=checkbox]");
                    //.attr("checked", true);
                }
                if (parseInt(parentli.getAttribute("data-level")) === 0) {
                    var check = $(parentli.firstElementChild).find("input[type=checkbox]");
                    check.attr("checked", true);
                }
            });
            $(label).prepend(check);
            $(label).prepend(span);
            
            var li = document.createElement("li");
            li.setAttribute("data-level", level);
            li.appendChild(label);
            if (Array.isArray(node[i][data.node])) {
                
                li.appendChild(addNode(node[i][data.node],level+1));
            }
            
            ul.appendChild(li);
        }
        level++;
        return ul;
    };

    this.treeview.appendChild(addNode(node));
};

