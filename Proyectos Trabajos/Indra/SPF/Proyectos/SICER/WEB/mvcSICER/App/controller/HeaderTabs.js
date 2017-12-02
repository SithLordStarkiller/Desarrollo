/**
 * This example demonstrates a Tab Panel with its Tab Bar rendered inside the panel header.
 * This configuration allows panel title and tabs to be displayed parallel to each other.
 */

// Ext.create('Ext.tab.Panel', {
//    width: 400,
//    height: 400,
//    renderTo: document.body,
//    items: [{
//        title: 'Foo'
//    }, {
//        title: 'Bar',
//        tabConfig: {
//            title: 'Custom Title',
//            tooltip: 'A button tooltip'
//        }
//    }]
//});



//Ext.create('Ext.tab.Panel', {
//    renderTo: Ext.getBody(),
//    tabPosition: 'left',
//    tabPosition: 'bottom',
//    width: 600,
//    height: 200,
////    listeners: {
////        beforetabchange: function(tabs, newTab, oldTab) {
////            return newTab.title != 'P2';
////        }
//    //    },

//    items: [{
//        title: 'P1'
//       
//    }, {
//        title: 'P2'
//    }, {
//        title: 'P3'
//    }]
//});


Ext.create('Ext.tab.Panel', {
    width: '100%',
    height: 200,
    activeTab: 0,
    bodyPadding: 10,
    tabPosition: 'top',
    items: [
        {
            title: 'Función 1',
           
            html: 'A simple tab'
        },
        {
            title: 'Función 2',
            html: 'Another one'
        }, {
            title: 'Función 3',
            html: 'A simple tab'
        },
        {
            title: 'Función 4',
            html: 'Another one'
        }, {
            title: 'Función 5',
            html: 'A simple tab'
        },
        {
            title: 'Función 6',
            html: 'Another one'
        },
    ],
    renderTo: Ext.getBody()
});


//Ext.define('app.controller.HeaderTabs', {
//    extend: 'Ext.container.Container',
//    xtype: 'header-tabs',

//    //<example>
//    exampleTitle: 'Header Tabs',
//    
//    //</example>

//    layout: {
//        type: 'hbox',
//        align: 'middle'
//    },
//    viewModel: true,

//    initComponent: function() {
//        Ext.apply(this, {
//            // width: this.themeInfo.width,
//            width: 2000,
//            items: [{
//                xtype: 'tabpanel',
//                title: 'Tab Panel',
//                flex: 1,
//                height: 500,
//                //icon: this.themeInfo.iconHeader,
//                //glyph: this.themeInfo.glyphHeader,
//                tabBarHeaderPosition: 2,
//                reference: 'tabpanel',
//                plain: true,
//                defaults: {
//                    bodyPadding: 10,
//                    scrollable: true,
//                    border: false
//                },
//                bind: {
//                   // headerPosition: '{positionBtn.value}',
//                   // tabRotation: '{tabRotationBtn.value}',
//                    // titleRotation: '{titleRotationBtn.value}',
//                     titleRotation: '1',
//                   // titleAlign: '{titleAlignBtn.value}',
//                   // iconAlign: '{iconAlignBtn.value}'
//                },
//                items: [{
//                    title: 'Tab 1'
//                   // icon: this.themeInfo.icon1,
//                   // glyph: this.themeInfo.glyph1,
//                  //  html: KitchenSink.DummyText.longText
//                }, {
//                    title: 'Tab 2'
//                   // icon: this.themeInfo.icon2,
//                   // glyph: this.themeInfo.glyph2,
//                   // html: KitchenSink.DummyText.extraLongText
//                }, {
//                    title: 'Tab 3'
//                   // icon: this.themeInfo.icon3,
//                   // glyph: this.themeInfo.glyph3,
//                   // html: KitchenSink.DummyText.longText
//                }]
//            }]
//        });

//        this.callParent();
//    }
//});