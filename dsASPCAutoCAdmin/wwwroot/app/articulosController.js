appadmin.controller('Articulos', function ($scope, Llamada, $timeout) {
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
    
    $scope.dataGridOptions = {
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
                dataField: "descripcion",
                width: 600,
                caption: "Descripcion"
            }, {
                caption: "",
                width: 100,
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "editTemplate"
            }/*, {
                caption: "",
                width: 80,
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "deleteTemplate"
            }*/
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
    $scope.guardarCambios = function (articulo) {
        
        $scope.datagrid.collapseAll(-1);
        articulo.idSeccion = $scope.lastMarca.idSeccion;
        articulo.idFamilia = $scope.lastModelo.idFamilia;
        articulo.idTipoVidrio = $scope.lastVidrio.idTipoVidrio;
        guardarCambios(articulo);
    };
    $scope.modificarArticulo = function (vid) {
        try {
            $scope.selectElement.option("value", undefined);
            $scope.selectElement3.option("value", undefined);
            $scope.selectElement2.option("value", undefined);

        } catch (ex) {
            console.log(ex);
            //alert("Error");
        }
        console.log(vid);
        $scope.popupVisible = true;
        $scope.currentarticulo = vid.data;
        $scope.lastMarca = {
            idSeccion: $scope.currentarticulo.idSeccion,
            descripcionSeccion: $scope.currentarticulo.descripcionSeccion,
        }
        $scope.lastModelo = {
            idFamilia: $scope.currentarticulo.idFamilia,
            descripcionFamilia: $scope.currentarticulo.descripcionFamilia,
        }
        $scope.lastVidrio = {
            idTipoVidrio: $scope.currentarticulo.idTipoVidrio,
            descripcion: $scope.currentarticulo.descripcionTipoVidrio,
        }
        /*if (NotNullNotUndefinedNotEmpty($scope.currentarticulo.idSeccion)) {
            buscaModelos($scope.lastMarca);
        }*/
        //console.log(Currentarticulo);
    };
    guardarCambios = function (art) {
        art.files = null;
        console.log(art);
        console.log("Hasta aqui he llegado!");
        console.log(art);
        Llamada.post("ArticulosModificar", art)
            .then(function (respuesta) {
                mensajeExito("Datos guardados con éxito");
                $scope.popupVisible = false;
            });
        $scope.popupVisible = false;
        

    };
    $scope.popupVisible = false;
    $scope.popupOptions = {
        width: 660,
        height: '90%',
        showTitle: true,
        title: "Modificar Artículo",
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
    $scope.selectBox = {
        dataSource: [],
        displayExpr: "descripcionSeccion",
        valueExpr: "idSeccion",
        searchEnabled: true,
        noDataText: 'No se ha encontrado ninguna marca',
        //value: products[0].idSeccion,
        fieldTemplate: 'field',
        onInitialized: function (e) {
            $scope.selectElement = e.component;
            oldValue = e.component.option("value");
        },
        onValueChanged: function (e) {
            console.log(e);
            if (NotNullNotUndefinedNotEmpty(e.component._options.selectedItem)) {
                console.log(e);
                console.log(e.component._options)
                console.log(e.component._options.selectedItem)
                $scope.lastMarca = JSON.parse("" + JSON.stringify(e.component._options.selectedItem));
                buscaModelos($scope.lastMarca);
            } else {
                console.log(e);
                console.log("Está vacío!!");
                oldValue = e.previousValue
            }

        }
    }
    $scope.selectBox2 = {
        dataSource: [],
        valueExpr: "idFamilia",
        placeholder: "Selecciona un modelo",
        noDataText: 'Selecciona la marca primero',
        displayExpr: "descripcionFamilia",
        //value: products[0].idFamilia,
        onInitialized: function (e) {
            $scope.selectElement2 = e.component;
        },
        onValueChanged: function (e) {
            if (NotNullNotUndefinedNotEmpty(e.component._options.selectedItem)) {
                console.log(e);
                console.log(e.component._options)
                console.log(e.component._options.selectedItem)
                $scope.lastModelo = JSON.parse("" + JSON.stringify(e.component._options.selectedItem));
                console.log("Mira el modelo:");
                console.log($scope.lastModelo);
            }

        }
    }

    $scope.selectBox3 = {
        dataSource: [],
        displayExpr: "descripcion",
        placeholder: "Selecciona un tipo de vidrio",
        noDataText: 'No se han encontrado vidrios',
        valueExpr: "idTipoVidrio",
        //searchEnabled: true,
        //value: products[0].idTipoVehiculo,
        //fieldTemplate: 'field',
        onInitialized: function (e) {
            $scope.selectElement3 = e.component;
            Llamada.get("TiposVidrioLeer")
                .then(function (respuesta) {
                    console.log(respuesta.data);
                    $scope.tiposvid = respuesta.data;
                    for (i = 0; i < $scope.tiposvid.length; i++) {
                        $scope.tiposvid[i].url = Llamada.getRuta($scope.tiposvid[i].imagen);
                    }
                    if (NotNullNotUndefinedNotEmpty($scope.selectElement3)) {
                        $scope.selectElement3.option("dataSource", $scope.tiposvid);
                    }

                })
        },
        onValueChanged: function (e) {
            console.log(e);
            $scope.lastVidrio = e.component._options.selectedItem;
            console.log(e.component._options.selectedItem);
        }
    }
    

    buscaMarcas = function (val) {
        console.log("Ahora  busca marcas");
        console.log(val);
        console.log($scope.newValor);
        Llamada.get("MarcasLeerPorCadena?cadena=" + $scope.newValor)
            .then(function (respuesta) {
                console.log(respuesta.data);
                console.log($scope.selectElement);
                $scope.marcas = respuesta.data;
                for (i = 0; i < $scope.marcas.length; i++) {
                    $scope.marcas[i].url = Llamada.getRuta($scope.marcas[i].imagen);
                }
                $scope.selectElement.option("dataSource", $scope.marcas);
                $scope.selectElement.open();
            })
    }
    buscaModelos = function (val) {
        console.log("Ahora  busca modelos");
        console.log(val);
        console.log($scope.newValor);
        Llamada.get("ModelosLeerPorMarca?IDSeccion=" + val.idSeccion)
            .then(function (respuesta) {
                console.log(respuesta.data);
                console.log($scope.selectElement);
                $scope.modelos = respuesta.data;
                for (i = 0; i < $scope.modelos.length; i++) {
                    $scope.modelos[i].url = Llamada.getRuta($scope.modelos[i].imagen);
                }
                $scope.selectElement2.option("dataSource", $scope.modelos);
                $scope.selectElement2.open();
            })
    }

    var inputChangedPromise;
    $scope.cambiobusqueda = function () {
        if (inputChangedPromise) {
            $timeout.cancel(inputChangedPromise);
        }
        inputChangedPromise = $timeout(buscaMarcas, 1000);
    }
    $scope.settingsbox = {
        placeholder: 'Selecciona una Marca',
        bindingOptions: {
            value: 'textBoxValue'
        },
        onInitialized: function (e) {
            $scope.campotexto = e.component;
        },
        valueChangeEvent: "keyup",
        onKeyUp(e) {
            $scope.newValor = e.component.option("value");
            $scope.cambiobusqueda();
        }
    }
    $scope.inittext = function (e) {
        alert("Holi");
        console.log(e);
        console.log("Aqui se inicializa");
        console.log(e);
        $scope.campotexto = e.component;
    }
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
        tipo: "Articulo",
        cadena: ""
    };
    LeerRegistros(obj);
    $scope.currentarticulo = {
        descripcion: "Descripción"
    };
    $scope.guardarCambiosPopup = function () {
        $scope.guardarCambios($scope.currentarticulo);
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
                })
        } else {
            $scope.resultadobusqueda = [];
            $scope.NumReg = 0;
            $scope.loading = false;
        }
    }
    
    /*$scope.anadirVidrio = function (res) {
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
        }
        $scope.currentcarroceria.vidrios.push(vid);
    }*/
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
            tipo: "Articulo",
            cadena: $scope.buscador,
        };
        LeerRegistros(obj);
    }
    $scope.anularBusqueda = function () {
        console.log("Ok, anulo búsqueda");
        $scope.buscador = "";
        var obj = {
            tipo: "Articulo",
            cadena: "",
            
        };
        LeerRegistros(obj);
    }
});