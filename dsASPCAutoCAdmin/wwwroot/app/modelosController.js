﻿appadmin.controller('Modelos', function ($scope, Llamada, $timeout) {
    $scope.cambiarPagina = function (sender, val) {
        cambiarBotonesPaginacion("");
        switch (val) {
            case "F":
                cambiarBotonesPaginacionIniciales("disabled");
                break;
            case "L":
                cambiarBotonesPaginacionFinales("disabled");
                break;
        }
        $scope.vm.cm.accionPagina = val;
        LeerRegistros($scope.vm.cm);
    };
    $scope.verModelo = function (a) {
        console.log($scope.cambios)
        if ($scope.cambios === true) {
            result = DevExpress.ui.dialog.confirm("¿Seguro que deseas dejar de modificar esta marca sin guardar los cambios?");
            result.then(function (val) {
                if (val) {
                    irAModelo(a);
                }
            });
        } else {
            irAModelo(a);
        }

    }
    $scope.verMarca = function (a) {
        irAMarca(a);
    };
    Llamada.get("MarcasLeerPorID?IdSeccion=" + parseInt(document.getElementById("idseccion").value))
        .then(function (respuesta) {
            $scope.currentmarca = respuesta.data;
            $scope.currentmarca.url = Llamada.getRuta($scope.currentmarca.imagen);
        });
    $scope.cancelar = function () {
        Llamada.get("MarcasLeerPorID?IdSeccion=" + parseInt(document.getElementById("idseccion").value))
            .then(function (respuesta) {
                $scope.currentmarca = respuesta.data;
                $scope.currentmarca.url = Llamada.getRuta($scope.currentmarca.imagen);
                $scope.cambios = false;
            });
    }
    $scope.showColumnLines = false;
    $scope.showRowLines = true;
    $scope.showBorders = true;
    $scope.rowAlternationEnabled = true;
    $scope.dataGridOptions = {
        bindingOptions: {
            showColumnLines: "showColumnLines",
            showRowLines: "showRowLines",
            showBorders: "showBorders",
            rowAlternationEnabled: "rowAlternationEnabled"
        },
        dataSource: [],
        keyExpr: "id",
        editing: {
            allowAdding: false, // Enables insertion
            allowDeleting: false, // Enables removing
            editEnabled: false
        },
        selection: {
            mode: "single"
        },
        /*masterDetail: {
            enabled: false,
            template: "detail"
        },*/
        onRowInserted: function (info, a) {
            console.log(info);
            console.log(a);
        },
        onInitNewRow: function (info, b) {
            console.log("init");
            console.log(info);
            info.data.descripcion = 'Nuevo';
            console.log("HOLIII");
            info.component.saveEditData();
        },
        /*onSelectionChanged: function (e) {
            e.component.collapseAll(-1);
            e.component.expandRow(e.currentSelectedRowKeys[0]);
        },*/
        onRowRemoving: function (e) {
            console.log(e);
            eliminarRegistro(e.data.idCarroceria);
        },
        columns: [
            {
                dataField: "url",
                caption: "Imagen",
                width: "30%",
                alignment: "center",
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "cellTemplate"
            }, {
                dataField: "descripcionFamilia",
                width: "30%",
                caption: "Descripcion"
            }, {
                dataField: "codigoFamilia",
                width: 100,
                caption: "Código"
            }, /*{
                caption: "",
                width: 100,
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "editTemplate"
            }, {
                caption: "",
                width: 80,
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "deleteTemplate"
            },*/ {
                caption: "",
                width: 30,
                alignment: "center",
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "verTemplate"
            }
        ],
        summary: {
            totalItems: [{
                column: "OrderNumber",
                summaryType: "count"
            }, {
                column: "OrderDate",
                summaryType: "min",
                customizeText: function (data) {
                    return "First: " + Globalize.formatDate(data.value, { date: "medium" });
                }
            }, {
                column: "SaleAmount",
                summaryType: "sum",
                valueFormat: "currency"
            }]
        },
        onInitialized: function (e) {
            console.log(e);
            $scope.datagrid = e.component;
        }
    };
    LeerRegistros = function (obj, objmodificado) {
        $scope.lastConsulta = JSON.parse("" + JSON.stringify(obj));
        Llamada.post("LecturasGenericasPaginadas", obj)
            .then(function (respuesta) {
                if (respuesta.data.articulos.length < 1) {
                    switch (obj.accionPagina) {
                        case "N":
                            cambiarBotonesPaginacionFinales("disabled");
                            break;
                        case "P":
                            cambiarBotonesPaginacionIniciales("disabled");
                            break;
                    }

                } else {
                    $scope.vm = respuesta.data;
                    $scope.orders = respuesta.data.articulos;
                    if (NotNullNotUndefinedNotEmpty(objmodificado)) {
                        $scope.orders.splice(0, 0, objmodificado);
                    }
                    for (i = 0; i < $scope.orders.length; i++) {
                        $scope.orders[i].url = Llamada.getRuta($scope.orders[i].imagen);
                    }
                    console.log($scope.orders);
                    console.log("Arriba las orders");
                    $scope.datagrid.option("dataSource", $scope.orders);
                }
                //$scope.datagrid.repaint();
            });
    };

    $scope.files = "";
    $scope.guardarCambios = function (marca) {
        if (NotNullNotUndefinedNotEmpty(marca.files)) {
            console.log(document.getElementById("filesup").files);
            var fd = new FormData();
            fd.append('file.jpg', document.getElementById("filesup").files[0]);
            console.log(fd);
            console.log(marca.files);
            //console.log($scope.files);
            marca.url = marca.files.data;
            //console.log(vidrio.files.data);
            Llamada.postFile(fd)
                .then(function (respuesta) {
                    marca.imagen = respuesta[0].contenido;
                    document.getElementById("filesup").value = null;
                    guardarCambios(marca);
                });
        } else {
            marca.newImagen = marca.imagen;
            guardarCambios(marca);
        }

        $scope.datagrid.collapseAll(-1);
    };
    /*$scope.modificarMarca = function (vid) {
            $scope.popupVisible = true;
            $scope.currentmarca = vid.data;
    };*/
    guardarCambios = function (carroceria) {
        carroceria.files = null;
        console.log(carroceria);
        Llamada.post("MarcasCrearModificar", carroceria)
            .then(function (respuesta) {
                mensajeExito("Datos guardados con éxito");
                
                console.log(respuesta);

                $scope.popupVisible = false;
            });
        $scope.popupVisible = false;

    };
    $scope.popupVisible = false;
    $scope.popupOptions = {
        width: 660,
        height: 540,
        showTitle: true,
        title: "Nueva Carrocería",
        dragEnabled: false,
        bindingOptions: {
            visible: 'popupVisible'
        },
        closeOnOutsideClick: true
    };
    $scope.cambioInput = function () {
        alert("Holi");
    };
    $scope.eliminarRegistro = function (id) {
        result = DevExpress.ui.dialog.confirm("¿Seguro que deseas eliminar esta carrocería?");
        result.then(function (val) {
            if (val) {


                Llamada.get("MarcasEliminar?idSeccion=" + id.data.idSeccion)
                    .then(function (respuesta) {
                        console.log(respuesta);

                        LeerRegistros($scope.lastConsulta);
                    });
            }
        });


    };
    $scope.hola = function () {
        alert("Hola");
    };
    var obj = {
        tipo: "Modelos",
        cadena: "",
        idSeccion: parseInt(document.getElementById("idseccion").value)
    };
    LeerRegistros(obj);
    /*$scope.currentmarca = {
        descripcionSeccion: "Descripción"
    };*/
    /*$scope.crearRegistro = function () {
        $scope.popupVisible = true;
        $scope.currentmarca = {
            descripcionSeccion: "Descripción"
        };
    };*/
    $scope.guardarCambiosPopup = function () {
        $scope.guardarCambios($scope.currentmarca);
    };
    $scope.cambiarPagina = function (sender, val) {
        cambiarBotonesPaginacion("");
        switch (val) {
            case "F":
                cambiarBotonesPaginacionIniciales("disabled");
                break;
            case "L":
                cambiarBotonesPaginacionFinales("disabled");
                break;
        }
        $scope.vm.cm.accionPagina = val;
        LeerRegistros($scope.vm.cm);
    };
    fnocultar = function () {
        $scope.mostrardesplegable = false;
    };
    $scope.focofuera = function (a, b) {
        $timeout(fnocultar, 1000);
    };
    $scope.entrafoco = function () {
        if (NotNullNotUndefinedNotEmpty($scope.resultadobusqueda)) {
            $scope.mostrardesplegable = true;
        }
    };
    buscarArticulos = function () {
        $scope.loading = true;
        if ($scope.cadena.length > 0) {
            Llamada.get("TiposVidrioLeerPorCadena?cadena=" + $scope.cadena)
                .then(function (respuesta) {
                    $scope.resultadobusqueda = respuesta.data;
                    //$scope.NumReg = respuesta.data.numReg;
                    $scope.mostrardesplegable = true;
                    $scope.loading = false;
                    document.getElementById("desplegable").style.display = "block";
                });
        } else {
            $scope.resultadobusqueda = [];
            $scope.NumReg = 0;
            $scope.loading = false;
        }
    }
    var inputChangedPromise;
    $scope.cambiobusqueda = function () {
        if (inputChangedPromise) {
            $timeout.cancel(inputChangedPromise);
        }
        inputChangedPromise = $timeout(buscarArticulos, 1000);
    }
    $scope.noEliminado = function (vidrio) {
        if (NotNullNotUndefinedNotEmpty(vidrio)) {
            if (NotNullNotUndefinedNotEmpty(vidrio.modificador)) {
                if (vidrio.modificador > 1) {
                    return false;
                } else {
                    return true;
                }
            } else {
                return true;
            }
        } else {
            return false;
        }
    }
    $scope.eliminarVidrio = function (vidrio, $index) {
        result = DevExpress.ui.dialog.confirm("¿Seguro que deseas eliminar este vidrio?");
        result.then(function (val) {
            if (val) {
                console.log("OK");
                $scope.$apply(function () {
                    vidrio.modificador = 2;
                });


                console.log(vidrio);
            }
        });
    }
    $scope.cancelarCambios = function () {
        $scope.popupVisible = false;
    }




    var buscaChangePromise;
    $scope.cambioBuscador = function () {
        if (buscaChangePromise) {
            $timeout.cancel(buscaChangePromise);
        }
        buscaChangePromise = $timeout($scope.activarBusqueda, 1000);
    }
    $scope.activarBusqueda = function () {
        console.log("Ok, busco con");
        console.log($scope.buscador);
        var obj = {
            tipo: "Modelos",
            cadena: $scope.buscador,
            idSeccion: parseInt(document.getElementById("idseccion").value)
        };
        LeerRegistros(obj);
    }
    $scope.anularBusqueda = function () {
        console.log("Ok, anulo búsqueda");
        $scope.buscador = "";
        var obj = {
            tipo: "Modelos",
            cadena: "",
            idSeccion: parseInt(document.getElementById("idseccion").value)
        };
        LeerRegistros(obj);
    }
});