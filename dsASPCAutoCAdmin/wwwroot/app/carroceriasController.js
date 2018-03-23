appadmin.controller('Carrocerias', function ($scope, Llamada, $timeout) {
    console.log("Holi");
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
    $scope.verMarca = function (a) {
        irAMarca(a);
    };
    $scope.showColumnLines = false;
    $scope.showRowLines = true;
    $scope.showBorders = true;
    $scope.rowAlternationEnabled = true;
    $scope.dataGridOptions = {
        dataSource: [],
        keyExpr: "id",
        editing: {
            allowAdding: false, // Enables insertion
            allowDeleting: false, // Enables removing
            editEnabled: false
        },
        bindingOptions: {
            showColumnLines: "showColumnLines",
            showRowLines: "showRowLines",
            showBorders: "showBorders",
            rowAlternationEnabled: "rowAlternationEnabled"
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
                dataField: "descripcion",
                width: 600,
                caption: "Descripción"
            }, {
                caption: "",
                width: "20%",
                allowFiltering: false,
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "editTemplate"
            }, {
                caption: "",
                width: "20%",
                allowFiltering: false,
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "deleteTemplate"
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
    $scope.guardarCambios = function (carroceria) {


        if (NotNullNotUndefinedNotEmpty(carroceria.files)) {
            console.log(document.getElementById("filesup").files);
            var fd = new FormData();
            fd.append('file.jpg', document.getElementById("filesup").files[0]);
            console.log(fd);
            console.log(carroceria.files);
            //console.log($scope.files);
            carroceria.url = carroceria.files.data;
            //console.log(vidrio.files.data);
            Llamada.postFile(fd)
                .then(function (respuesta) {
                    carroceria.imagen = respuesta[0].contenido;
                    document.getElementById("filesup").value = null;
                    guardarCambios(carroceria);
                });
        } else {
            carroceria.newImagen = carroceria.imagen;
            guardarCambios(carroceria);
        }



        //guardarCambios(carroceria);
        //$scope.datagrid.collapseAll(-1);
    };
    $scope.guardarCambiosModelo = function () {
        if (NotNullNotUndefinedNotEmpty($scope.currentmodelo.files)) {
            console.log(document.getElementById("filesup").files);
            var fd = new FormData();
            fd.append('file.jpg', document.getElementById("filesup").files[0]);
            console.log(fd);
            console.log($scope.currentmodelo.files);
            //console.log($scope.files);
            $scope.currentmodelo.url = $scope.currentmodelo.files.data;
            //console.log(vidrio.files.data);
            Llamada.postFile(fd)
                .then(function (respuesta) {
                    $scope.currentmodelo.imagen = respuesta[0].contenido;
                    document.getElementById("filesup").value = null;
                    guardarCambiosModelo($scope.currentmodelo);
                });
        } else {
            $scope.currentmodelo.newImagen = $scope.currentmodelo.imagen;
            guardarCambiosModelo($scope.currentmodelo);
        }

        $scope.datagrid.collapseAll(-1);
    };
    guardarCambiosModelo = function (modelo) {
        modelo.files = null;
        console.log(modelo);
        Llamada.post("ModelosCrearModificar", modelo)
            .then(function (respuesta) {
                mensajeExito("Datos guardados con éxito");
                console.log(respuesta);
            });
    };
    $scope.modificarCarroceria = function (vid) {
        console.log(vid);
        Llamada.get("CarroceriasLeerPorID?IDCarroceria=" + vid.data.idCarroceria)
            .then(function (respuesta) {
                $scope.popupVisible = true;
                $scope.currentcarroceria = respuesta.data;
                $scope.currentcarroceria.url = Llamada.getRuta($scope.currentcarroceria.imagen);
            });
    };
    guardarCambios = function (carroceria) {
        carroceria.files = null;
        console.log(carroceria);
        Llamada.post("CarroceriasCrearModificar", carroceria)
            .then(function (respuesta) {
                mensajeExito("Datos guardados con éxito");
                if (ZeroSiNull(carroceria.idCarroceria) < 1) {
                    var obj = {
                        tipo: "Carrocerias",
                        cadena: "",
                        accionPagina: "N",
                        lastValor: carroceria.descripcion,
                        lastIndice: respuesta.data.identidad
                    };
                    carroceria.idCarroceria = respuesta.identidad;
                    LeerRegistros(obj, carroceria);
                }
                console.log(respuesta);

                $scope.popupVisible = false;
            });
        $scope.popupVisible = false;

    };
    $scope.popupVisible = false;
    $scope.popupOptions = {
        width: "98%",
        height: "98%",
        showTitle: true,
        title: "Carrocería",
        fullScreen:false,
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
                

                Llamada.get("CarroceriasEliminar?idCarroceria=" + id.data.idCarroceria)
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
        tipo: "Carrocerias",
        cadena: ""
    };
    LeerRegistros(obj);
    $scope.currentcarroceria = {
        descripcion: "Descripción"
    };
    $scope.crearRegistro = function () {
        $scope.popupVisible = true;
        $scope.currentcarroceria = {
            descripcion: "",
            url: Llamada.getRuta(""),
        };
    };
    $scope.guardarCambiosPopup = function () {
        $scope.guardarCambios($scope.currentcarroceria);
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
    };
    var inputChangedPromise;
    $scope.cambiobusqueda = function () {
        if (inputChangedPromise) {
            $timeout.cancel(inputChangedPromise);
        }
        inputChangedPromise = $timeout(buscarArticulos, 1000);
    };
    $scope.anadirVidrio = function (res) {
        console.log(res);
        $scope.mostrardesplegable = false;
        if (!NotNullNotUndefinedNotEmpty($scope.currentcarroceria.vidrios)) {
            $scope.currentcarroceria.vidrios = [];
        }
        var vid = {
            IDVidrio: 0,
            Imagen: res.imagen,
            descripcion: res.descripcion,
            IdTipoVidrio: res.idTipoVidrio,
            modificador: 1
        };
        $scope.currentcarroceria.vidrios.push(vid);
    };
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
    };
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
    };
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
            tipo: "Carrocerias",
            cadena: $scope.buscador,
        };
        LeerRegistros(obj);
    }
    $scope.anularBusqueda = function () {
        console.log("Ok, anulo búsqueda");
        $scope.buscador = "";
        var obj = {
            tipo: "Carrocerias",
            cadena: "",

        };
        LeerRegistros(obj);
    }


});