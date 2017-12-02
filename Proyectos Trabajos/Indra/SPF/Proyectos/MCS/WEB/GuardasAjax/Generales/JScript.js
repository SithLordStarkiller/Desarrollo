// JScript File

function onFocus(objeto)
{
    objeto.style.backgroundColor = "#FFFF99";
    
}
//***********************************************************************************************************************************
function onLosFocus(objeto)
{
    objeto.style.backgroundColor = "#FFFFFF";
    objeto.value = objeto.value.toUpperCase();
}
//***********************************************************************************************************************************
function onLostFocus(objeto)
{
    objeto.style.backgroundColor = "#FFFFFF";
}
//*********************************************************************************************************************************
 function deshabilitar(chk, txt){        
       if (chk.checked==true) {
           txt.disabled = false;
           //txt.className = '';
           txt.focus();
          
      }else{
           txt.disabled = true;
           //txt.className = 'disabled';
      }
  }      

//***********************************************************************************************************************************
function svalidar(e) { // 1
    tecla = (document.all) ? e.keyCode : e.which; // 2
    if ((tecla==8) || (tecla==9) || (tecla==0) || (tecla==127) ) return true; // 3
    patron =/[a-zA-Z\s]/; // 4
    te = String.fromCharCode(tecla); // 5
    return patron.test(te); // 6
} 
//***********************************************************************************************************************************
function validarNoEscritura(e)
{ // 1
        tecla = (document.all) ? e.keyCode : e.which; // 2
        if ((tecla==8) || (tecla==9) || (tecla==0) || (tecla==127) ) return true; // 3
        patron =/[\s]/; // 4        
        te = String.fromCharCode(tecla); // 5
        if (event.keyCode==32) event.returnValue = false;
        return patron.test(te); // 6
 }
 
//*********************************************************************************************************************************** 
//Validar Solo Números
 function nvalidar(e) 
{ // 1
    tecla = (document.all) ? e.keyCode : e.which; // 2
        
    if ((tecla==8) || (tecla==9) || (tecla==0) || (tecla==127) ) return true; // 3
    if(tecla==32) return false;
    patron =/[0-9\s]/; // 4
    te = String.fromCharCode(tecla); // 5
    return patron.test(te); // 6
}
//***********************************************************************************************************************************
//Validar Solo Cantidades
function cvalidar(e) 
{ // 1
    tecla = (document.all) ? e.keyCode : e.which; // 2
    if ((tecla==8) || (tecla==9) || (tecla==0) || (tecla==127) ) return true; // 3
    patron =/[0-9.\s]/; // 4
    te = String.fromCharCode(tecla); // 5
    return patron.test(te); // 6
}
//***********************************************************************************************************************************
//Validar Solo nombres de usuarios
function uvalidar(e) 
{ // 1
    tecla = (document.all) ? e.keyCode : e.which; // 2
    if ((tecla==8) || (tecla==9) || (tecla==0) || (tecla==127) ) return true; // 3
    patron =/[a-zA-Z.\s]/; // 4
    te = String.fromCharCode(tecla); // 5
    return patron.test(te); // 6
}
//***********************************************************************************************************************************
//Validar Solo Texto
function validar(e)
{ // 1
    tecla = (document.all) ? e.keyCode : e.which; // 2
    if ((tecla==8) || (tecla==9) || (tecla==0) || (tecla==127) ) return true; // 3
    patron =/[A-Za-zñÑáéíóúÁÉÍÓÚ\s]/; // 4
    te = String.fromCharCode(tecla); // 5
    return patron.test(te); // 6
}
//***********************************************************************************************************************************
function avalidar(e)
//Validar Texto y Números
{ // 1
    tecla = (document.all) ? e.keyCode : e.which; // 2
    if ((tecla==8) || (tecla==9) || (tecla==0) || (tecla==127) ) return true; // 3
    patron =/[A-Za-zñÑáéíóúÁÉÍÓÚ0-9\s]/; // 4
    te = String.fromCharCode(tecla); // 5
    return patron.test(te); // 6
}
//***********************************************************************************************************************************
function hvalidar(e)
//Validar Texto y Números
{ // 1
    tecla = (document.all) ? e.keyCode : e.which; // 2
    if ((tecla==8) || (tecla==9) || (tecla==0) || (tecla==127) ) return true; // 3
    patron =/[0-9:\s]/; // 4
    te = String.fromCharCode(tecla); // 5
    return patron.test(te); // 6
}
//***********************************************************************************************************************************
function fvalidar(e)
//Validar Texto y Números
{ // 1
    tecla = (document.all) ? e.keyCode : e.which; // 2
    if ((tecla==8) || (tecla==9) || (tecla==0) || (tecla==127) ) return true; // 3
    patron =/[0-9/\s]/; // 4
    te = String.fromCharCode(tecla); // 5
    return patron.test(te); // 6
}
//***********************************************************************************************************************************
function oficioN(e)
//Validar Texto y Números y los caracteres /-
{ // 1
 tecla = (document.all) ? e.keyCode : e.which; // 2
    if ((tecla==8) || (tecla==9) || (tecla==0) || (tecla==127)) return true; // 3


    if (tecla == 13) {return false;}

    patron =/[\/\-A-Za-zñÑáéíóúÁÉÍÓÚ0-9\s]/; // 4
    te = String.fromCharCode(tecla); // 5
    return patron.test(te); // 6
}
//***********************************************************************************************************************************
function quitaacentos(t)
{
    á="a";é="e";í="i";ó="o";ú="u";Á="A";É="E";Í="I";Ó="O";Ú="U";
    acentos=/[áéíóúÁÉÍÓÚ]/g;
    return t.replace(acentos,
    function($1)
    {  return eval($1) }
    );
} 
//***********************************************************************************************************************************
//Contar caracteres, poner un limite
function Count(text,long) 
{
      var maxlength = new Number(long); // Change number to your max length.

      if (text.value.length > maxlength)
      {

            text.value = text.value.substring(0,maxlength);
      }
}
//***********************************************************************************************************************************
function cargarDias(objMes, objDia)
    {
        var dia = document.getElementById(objDia);
        dia.length = null;
        var index;
        
        if(objMes[1].selected == true)
        {
            for (index=1;index<=28;index=index+1)
            {
                dia[index - 1] = new Option(index, index,"","");
            }
        }
        else if(objMes[3].selected == true || objMes[5].selected == true || objMes[8].selected == true || objMes[10].selected == true)
        {
            for (index=1;index<=30;index=index+1)
            {
                dia[index - 1] = new Option(index, index,"","");
            }
        }
        else
        {
            for (index=1;index<=31;index=index+1)
            {
                dia[index - 1] = new Option(index, index,"","");
            }
        }
        
        dia.disabled = false;
    }
    //***********************************************************************************************    
//Validar una cantidad de tipo Double    
        function dvalidar(e) 
        { // 1
            tecla = (document.all) ? e.keyCode : e.which; // 2
            patron =/[0-9\-.\s]/; // 4/^-?[0-9]+([,\.][0-9]*)?$/
            te = String.fromCharCode(tecla); // 5
            return patron.test(te); // 6
        }
//***********************************************************************************************************************************    
function trim(cadena)
{
    return cadena.replace(/^\s+/g,'').replace(/\s+$/g,'');
}

    window.moveTo(0,0);
    if(document.all)
    {
        top.window.resizeTo(screen.availWidth,screen.availHeight);
    }
    else if(document.layers || document.getElementById)
    {
        if(top.window.outerHeight < screen.availHeight || top.window.outerWidth < screen.availWidth)
        {
            top.window.outerHeight = screen.availHeight;
            top.window.outerWidth = screen.availWidth;
        }
    }
    
    
    
    
       var message="SSP - Servicio de Protección Federal";
        function clickIE4(){
              if (event.button==2){
                    alert(message);
                    return false;
              }
        }
         
        function clickNS4(e){
        if (document.layers||document.getElementById&&!document.all){
                    if (e.which==2||e.which==3){
                          alert(message);
                          return false;
                    }
              }
        }
         
        if (document.layers){
              document.captureEvents(Event.MOUSEDOWN);
              document.onmousedown=clickNS4;
          }
        else if (document.all&&!document.getElementById){
              document.onmousedown=clickIE4;
          }
              
        document.oncontextmenu=new Function("alert(message);return false")
        //*********************************************************************************************************************************** 
//Validar Solo Números adicional
function ValidarNumero(e, campo)   
{   
key = e.keyCode ? e.keyCode : e.which         
if ((key == 8) || (key == 9) || (key == 0) || (key == 127))   return true ;   

 
if (key >= 47 && key <= 58) {      
if (campo.value == "") return true        
regexp = /,[0-9]{2}$/   
return !(regexp.test(campo.value))}

   if (key == 46) {
    if (campo.value == "") return false;    
regexp = /^-*[0-9]+$/
    return regexp.test(campo.value);
    }       
return false
}