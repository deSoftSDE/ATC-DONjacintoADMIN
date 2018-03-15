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
    $scope.guardarCambios = function (carroceria) {
        guardarCambios(carroceria);
        $scope.datagrid.collapseAll(-1);
    };
    $scope.modificarArticulo = function (vid) {
        console.log(vid);
        $scope.popupVisible = true;
        $scope.currentarticulo = vid.data;
    };
    guardarCambios = function (art) {
        art.files = null;
        console.log(art);
        alert("Hasta aqui he llegado!");
        if (false) {
            Llamada.post("ArticulosModificar", art)
                .then(function (respuesta) {
                    mensajeExito("Datos guardados con éxito");
                    if (ZeroSiNull(art.idArticulo) < 1) {
                        var obj = {
                            tipo: "Articulo",
                            cadena: "",
                            accionPagina: "N",
                            lastValor: carroceria.descripcion,
                            lastIndice: respuesta.data.identidad
                        };
                        art.idArticulo = respuesta.identidad;
                        LeerRegistros(obj, art);
                    }
                    console.log(respuesta);

                    $scope.popupVisible = false;
                });
            $scope.popupVisible = false;
        }
        

    };
    $scope.popupVisible = false;
    $scope.popupOptions = {
        width: 660,
        height: 540,
        showTitle: true,
        title: "Nueva Carrocería",
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






    var products = [{
        "ID": 1,
        "Name": "HD Video Player",
        "Price": 330,
        "Current_Inventory": 225,
        "Backorder": 0,
        "Manufacturing": 10,
        "Category": "Video Players",
        "ImageSrc": "../images/escudo.png"
    }, {
        "ID": 2,
        "Name": "SuperHD Player",
        "Price": 400,
        "Current_Inventory": 150,
        "Backorder": 0,
        "Manufacturing": 25,
        "Category": "Video Players",
        "ImageSrc": "../images/escudo.png"
    }, {
        "ID": 3,
        "Name": "SuperPlasma 50",
        "Price": 2400,
        "Current_Inventory": 0,
        "Backorder": 0,
        "Manufacturing": 0,
        "Category": "Televisions",
        "ImageSrc": "../images/escudo.png"
    }, {
        "ID": 4,
        "Name": "SuperLED 50",
        "Price": 1600,
        "Current_Inventory": 77,
        "Backorder": 0,
        "Manufacturing": 55,
        "Category": "Televisions",
        "ImageSrc": "../images/escudo.png"
    }, {
        "ID": 5,
        "Name": "SuperLED 42",
        "Price": 1450,
        "Current_Inventory": 445,
        "Backorder": 0,
        "Manufacturing": 0,
        "Category": "Televisions",
        "ImageSrc": "../images/escudo.png"
    }, {
        "ID": 6,
        "Name": "SuperLCD 55",
        "Price": 1350,
        "Current_Inventory": 345,
        "Backorder": 0,
        "Manufacturing": 5,
        "Category": "Televisions",
        "ImageSrc": "../images/escudo.png"
    }, {
        "ID": 7,
        "Name": "SuperLCD 42",
        "Price": 1200,
        "Current_Inventory": 210,
        "Backorder": 0,
        "Manufacturing": 20,
        "Category": "Televisions",
        "ImageSrc": "../images/escudo.png"
    }, {
        "ID": 8,
        "Name": "SuperPlasma 65",
        "Price": 3500,
        "Current_Inventory": 0,
        "Backorder": 0,
        "Manufacturing": 0,
        "Category": "Televisions",
        "ImageSrc": "../images/escudo.png"
    }, {
        "ID": 9,
        "Name": "SuperLCD 70",
        "Price": 4000,
        "Current_Inventory": 95,
        "Backorder": 0,
        "Manufacturing": 5,
        "Category": "Televisions",
        "ImageSrc": "../images/escudo.png"
    }, {
        "ID": 10,
        "Name": "DesktopLED 21",
        "Price": 175,
        "Current_Inventory": 0,
        "Backorder": 425,
        "Manufacturing": 75,
        "Category": "Monitors",
        "ImageSrc": "../images/escudo.png"
    }, {
        "ID": 11,
        "Name": "DesktopLED 19",
        "Price": 165,
        "Current_Inventory": 425,
        "Backorder": 0,
        "Manufacturing": 110,
        "Category": "Monitors",
        "ImageSrc": "../images/escudo.png"
    }, {
        "ID": 12,
        "Name": "DesktopLCD 21",
        "Price": 170,
        "Current_Inventory": 210,
        "Backorder": 0,
        "Manufacturing": 60,
        "Category": "Monitors",
        "ImageSrc": "../images/escudo.png"
    }, {
        "ID": 13,
        "Name": "DesktopLCD 19",
        "Price": 160,
        "Current_Inventory": 150,
        "Backorder": 0,
        "Manufacturing": 210,
        "Category": "Monitors",
        "ImageSrc": "../images/escudo.png"
    }, {
        "ID": 14,
        "Name": "Projector Plus",
        "Price": 550,
        "Current_Inventory": 0,
        "Backorder": 55,
        "Manufacturing": 10,
        "Category": "Projectors",
        "ImageSrc": "../images/escudo.png"
    }, {
        "ID": 15,
        "Name": "Projector PlusHD",
        "Price": 750,
        "Current_Inventory": 110,
        "Backorder": 0,
        "Manufacturing": 90,
        "Category": "Projectors",
        "ImageSrc": "../images/escudo.png"
    }, {
        "ID": 16,
        "Name": "Projector PlusHT",
        "Price": 1050,
        "Current_Inventory": 0,
        "Backorder": 75,
        "Manufacturing": 57,
        "Category": "Projectors",
        "ImageSrc": "../images/escudo.png"
    }];




    $scope.selectBox = {
        dataSource: products,
        displayExpr: "Name",
        valueExpr: "idSeccion",
        searchEnabled: true,
        value: products[0].idSeccion,
        fieldTemplate: 'field',
        onInitialized: function (e) {
            $scope.selectElement = e.component;
        },
        onValueChanged: function (e) {
            console.log(e);
            console.log(e.component._option)
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
        placeholder: 'Select a stateeeeee',
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
    $scope.crearRegistro = function () {
        $scope.popupVisible = true;
        $scope.currentarticulo = {
            descripcion: "Descripción"
        };
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
});