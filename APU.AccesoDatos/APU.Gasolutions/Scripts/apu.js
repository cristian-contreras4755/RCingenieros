if (typeof String.prototype.trim !== 'function') {
    String.prototype.trim = function () {
        return this.replace(/^\s+|\s+$/g, '');
    };
}
window.onload = function () {
    var ayuda = $('.imgAyuda');
    if (ayuda != null) {
        $('.imgAyuda').tooltip({ trigger: 'click' })
            .on('mouseleave', function () {
                $('.imgAyuda').tooltip('hide');
            });
    }
};
function Desbloquear(z) {
    z = (z == null) ? 100001 : z;
    var tiempo = 0;
    var procesando = document.getElementById('divProcesando');
    var contenedor = document.getElementById('contenedor');
    if (procesando != null) {
        window.setTimeout('document.getElementById("divProcesando").style.display="none"; document.getElementById("divProcesando").style.zIndex=' + z + '; document.getElementById("divCargando").style.display="none"; document.getElementById("contenedor").style.display="";', tiempo);
    }
}
function Bloquear(z) {
    z = (z == null) ? 100001 : z;
    var procesando = document.getElementById('divProcesando');
    var contenedor = document.getElementById('contenedor');
    var heights = document.documentElement.clientHeight;
    var body = document.body,
        html = document.documentElement;

    var height = Math.max(body.scrollHeight, body.offsetHeight, html.clientHeight, html.scrollHeight, html.offsetHeight);

    if (procesando != null) {
        document.getElementById('divProcesando').style.zIndex = z;
        document.getElementById('divProcesando').style.height = height + 'px';
        document.getElementById('divProcesando').style.display = '';
        document.getElementById('divCargando').style.display = '';
    }
}
function ValidarCorreo(correo) {
    //var exp = /^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$/;
    var exp = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,4})+$/;
    return exp.test(correo);
}
function MostrarValidacion(control, mensaje, posicion, tiempo) {
    /// <summary>Muestra un mensaje de validación a través del uso del tooltip de bootstrap.</summary>
    /// <param name="control" type="object">Control HTML a validar.</param>
    /// <param name="mensaje" type="string">Mensaje a mostrar en la validación.</param>
    /// <param name="position" type="string">Posición del mensaje de validación: top, bottom, left, right.</param>
    /// <param name="tiempo" type="int">Tiempo (en ms) durante el cual se mostrará la alerta.</param>
    /// <returns type="Bollean">false para impedir la ejecución.</returns>
    posicion = (posicion == undefined || posicion == '') ? 'top' : posicion;
    tiempo = tiempo == undefined ? 2000 : tiempo;
    $(control).data('title', mensaje).tooltip({ placement: posicion }).focus();
    $(control).addClass('txtObligatorio');
    setTimeout(function () {
        $(control).data('title', mensaje).tooltip('destroy');
        $(control).removeClass('txtObligatorio');
    }, tiempo);

    // Desbloquear Pantalla
    Desbloquear();
    return false;
}
function getRootWebSitePath() {
    var _location = document.location.toString();
    var applicationNameIndex = _location.indexOf('/', _location.indexOf('://') + 3);
    var applicationName = _location.substring(0, applicationNameIndex) + '/';
    var webFolderIndex = _location.indexOf('/', _location.indexOf(applicationName) + applicationName.length);
    var webFolderFullPath = _location.substring(0, webFolderIndex);

    return webFolderFullPath;
}
function ValidarCorreo(correo) {
    var exp = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,4})+$/;
    return exp.test(correo);
}
function FormatoFecha(fecha) {
    var dd = fecha.getDate();
    var mm = fecha.getMonth() + 1;
    var yyyy = fecha.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }
    if (mm < 10) {
        mm = '0' + mm;
    }
    var fechaddmmyyyy = dd + '/' + mm + '/' + yyyy;
    return fechaddmmyyyy;
}
function SeleccionarFila(row) {
    var _rows = row.parentNode.children;
    try {
        for (var i = 0; i < _rows.length; i++) {
            if (i % 2 == 1) {
                _rows[i].className = 'filaParGrid';
            } else {
                _rows[i].className = 'filaImparGrid';
            }
        }
    }
    catch (e) { }
    row.className = 'filaSeleccionadaGrid';
    return row;
}
function DeseleccionarFilas(grid) {
    if (grid != null) {
        var _rows = grid.children[0].children;
        try {
            for (var i = 0; i < _rows.length; i++) {
                if (i % 2 == 1) {
                    _rows[i].className = 'filaParGrid';
                } else {
                    _rows[i].className = 'filaImparGrid';
                }
            }
        }
        catch (e) { }
    }
}
function RetornarCelda(row, indice) {
    var celda = row.children[indice];
    return celda;
}
function RetornarCeldaValor(row, indice) {
    var celda = row.cells[indice].innerText.trim();
    return celda;
}
function MostrarMensaje(mensaje) {
    alert(mensaje);
}
function LimpiarCombo(combo) {
    while (combo.length > 0) {
        combo.remove(0);
    }
}
function LlenarCombo(combo, lista, columnaValor, columnaTexto, limpiarCombo) {
    /// <summary>Llena un combo con el vaor devuelto en un WebMethod.</summary>
    /// <param name="combo" type="object">Control a llenar.</param>
    /// <param name="lista" type="string">Lista generica con la informacion a llenar.</param>
    /// <param name="columnaValor" type="string">DataValue del Combo.</param>
    /// <param name="columnaTexto" type="string">DataField del combo.</param>
    if (limpiarCombo == null || limpiarCombo) LimpiarCombo(combo);

    for (var i = 0; i < lista.length; ++i) {
        var row = lista[i];
        AgregarOptionACombo(combo, row[columnaValor], row[columnaTexto]);
    }
}
function LlenarCombo2Textos(combo, lista, columnaValor, columnaTexto1, columnaTexto2, separador, limpiarCombo) {
    /// <summary>Llena un combo con el vaor devuelto en un WebMethod.</summary>
    /// <param name="combo" type="object">Control a llenar.</param>
    /// <param name="lista" type="string">Lista generica con la informacion a llenar.</param>
    /// <param name="columnaValor" type="string">DataValue del Combo.</param>
    /// <param name="columnaTexto" type="string">DataField del combo.</param>
    if (limpiarCombo == null || limpiarCombo) LimpiarCombo(combo);

    for (var i = 0; i < lista.length; ++i) {
        var row = lista[i];
        AgregarOptionACombo(combo, row[columnaValor], row[columnaTexto1] + separador + columnaTexto2);
    }
}
function LlenarCombo2Textos(combo, lista, columnaValor, columnaTexto1, columnaTexto2, limpiarCombo) {
    if (limpiarCombo == null || limpiarCombo) LimpiarCombo(combo);
    for (var i = 0; i < lista.length; ++i) {
        var row = lista[i];
        AgregarOptionACombo(combo, row[columnaValor], row[columnaTexto1] + ' - ' + row[columnaTexto2]);
    }
}
function LlenarCombo3Textos(combo, lista, columnaValor, columnaTexto1, columnaTexto2, columnaTexto3, limpiarCombo) {
    if (limpiarCombo == null || limpiarCombo) LimpiarCombo(combo);
    for (var i = 0; i < lista.length; ++i) {
        var row = lista[i];
        AgregarOptionACombo(combo, row[columnaValor], row[columnaTexto1] + ' ' + row[columnaTexto2] + ' ' + row[columnaTexto3]);
    }
}
function AgregarOptionACombo(combo, columnaValor, columnaTexto, seleccionado) {
    var option = document.createElement('option');
    option.value = columnaValor;
    option.text = columnaTexto;

    if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
        combo.add(option);
    } else {
        combo.add(option, null);
    }

    if (seleccionado != null && seleccionado) combo.value = columnaValor;
}
$(document).ready(function () {
    var options = { aSep: '', mDec: 0 };
    $('.txtStandarEntero').autoNumeric('init', options);
});
$(document).ready(function () {
    var options = { aSep: '', lZero: 'keep', mDec: 0, vMax: 99999999 };
    $('.txtStandarIdentificacion').autoNumeric('init', options);
});
$(document).ready(function () {
    var options = { aSep: '', mDec: 0, vMax: 9999999 };
    $('.txtStandarTelefono').autoNumeric('init', options);
});
$(document).ready(function () {
    var options = { aSep: '', mDec: 0, vMax: 999999999 };
    $('.txtStandarCelular').autoNumeric('init', options);
});
$(document).ready(function () {
    var options = { aSep: ',', mDec: 0 };
    $('.txtStandarCantidad').autoNumeric('init', options);
});
$(document).ready(function () {
    var options = { aSep: ',', aDec: '.', mDec: 2 };
    var inputArray = $('.txtStandarMonto');
    for (var i = 0; i < inputArray.length; i++) {
        if (inputArray[i].nodeName == 'INPUT' && inputArray[i].value != '') {
            inputArray[i].value = inputArray[i].value.replace(',', '');
        }
    }
    $('.txtStandarMonto').autoNumeric('init', options);
    $('.txtStandarMonto').autoNumeric('update', options);
});
$(document).ready(function () {
    var options = { aSep: ',', aDec: '.', aSign: 'S/. ', mDec: 2, pSign: 'p' };
    $('.txtStandarMontoSoles').autoNumeric('init', options);
});
$(document).ready(function () {
    var options = { aSep: ',', aDec: '.', aSign: 'S/. ', mDec: 2, pSign: 'p', vMin: 0, vMax: 99999999 };
    $('.txtStandarIngreso').autoNumeric('init', options);
});
$(document).ready(function () {
    var options = { aSep: ',', aDec: '.', aSign: '$ ', mDec: 2, pSign: 'p' };
    $('.txtStandarMontoDolares').autoNumeric('init', options);
});
$(document).ready(function () {
    var options = { aSep: ',', aDec: '.', aSign: '%', mDec: 2, pSign: 's' };
    $('.txtStandarPorcentaje').autoNumeric('init', options);
});
$(document).ready(function () {
    $('.txtStandarFecha').mask('00/00/0000', { reverse: true }, { placeholder: '__/__/__' });
});
$(document).ready(function () {
    $('.txtStandarHora').mask('00:00:00', { reverse: true }, { placeholder: '__:__:__' });
});
$(document).ready(function () {
    var options = { aSep: ',', aDec: '.', aSign: '%', mDec: 2, pSign: 's', vMax: 999.99 };
    $('.txtStandarTasa').autoNumeric('init', options);
});
function FormatoDecimal(numero, decimales, simbolo) {
    var options = { symbol: simbolo, decimal: ".", thousand: ",", precision: decimales, format: "%s%v" };
    return accounting.formatMoney(numero, options);
}
function FormatoMonetario(control, decimales, simbolo) {
    /// <summary>Asigna el formato Monetario a un control.</summary>
    /// <param name="control" type="object">Control HTML a formatear.</param>
    /// <param name="decimales" type="string">Cantidad de digitos decimales.</param>
    /// <param name="simbolo" type="string">Simbolo de la moneda de la operación.</param>
    /// <returns type="null">No retorna un valor, solo asigna una propiedad a un control.</returns>
    var options = { aSep: ',', aDec: '.', aSign: simbolo, mDec: decimales, pSign: 'p' };
    $('#' + control.id).autoNumeric('init', options);
    $('#' + control.id).autoNumeric('update', options);
}
function AbrirVentana(url, title, w, h) {
    var left = (screen.width / 2) - (w / 2);
    var top = (screen.height / 2) - (h / 2);
    return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=1, copyhistory=yes, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left).focus();
}

function AbrirVentana(url, title, w, h, scroll) {
    var left = (screen.width / 2) - (w / 2);
    var top = (screen.height / 2) - (h / 2);
    return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=' + (scroll ? '1' : '0') + ', resizable=1, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left).focus();
}

function ValidarRuc(ruc) {
    if (ruc.length != 11) {
        return false;
    }
    var pesos = [5, 4, 3, 2, 7, 6, 5, 4, 3, 2];
    var pesoTotal = 0;
    var valorControl = 0;
    var caracter = '';
    for (var i = 0; i < ruc.length - 1; i++) {
        caracter = ruc.charAt(i);
        pesoTotal = pesoTotal + (parseInt(caracter) * pesos[i]);
    }
    var valorAsociado = Math.floor(pesoTotal / 11);

    valorControl = pesoTotal - (valorAsociado * 11);

    var valorPivot = 11 - valorControl;

    switch (valorPivot) {
    case 10: valorPivot = 0; break;
    case 11: valorPivot = 1; break;
    }

    if (valorPivot == parseInt(ruc.charAt(10))) {
        return true;
    }
    else { return false; }
}