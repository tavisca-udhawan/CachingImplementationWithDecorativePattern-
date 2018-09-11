Hotel = {};
var addHotel = () => {
    var name = $('#name').val();
    var price = $('#price').val();
    var address = $('#address').val();
    var rating = $('#rating').val();
    Hotel.Name = name;
    Hotel.Price = price;
    Hotel.Address = address;
    Hotel.StarRating = rating;
    HotelCall(Hotel);
}
var HotelCall = (Hotel) => {
    $.ajax({
        url: "/api/hotel/add",
        data: JSON.stringify(Hotel),
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        async: false,
        success: function (result) {
            console.log("success");

        },
        error: function (reason) {
            console.log("had a failure");
            console.log(reason);
        }
    }).done((response) => { appendData(response) });
};
User = {};
var authenticate = () => {
    var username = $("#username").val();
    var password = $("#password").val();

    User.UserName = username;
    User.Password = password;

    ajaxCall(User);

};

var appendData = (response) => {
    $("#showResponse").append("<p>" + response.Id + "</p>")
}

var ajaxCall = (User) => {

    $.ajax({
        url: "/api/authenticate",
        data: JSON.stringify(User),
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        async: false,
        success: function (result) {
            console.log("success");
            if (result.UserType == 0)
                window.location.href = "AdminIndex.html";
            else if (result.UserType == 1)
                window.location.href = "UserIndex.html";
        },
        error: function (reason) {
            console.log("had a failure");
            console.log(reason);
        }
    }).done((response) => { appendData(response) });
};

//var GetHotel = () => {
//    var id = $("#hotelId").val();
//    HotelAjaxCall(id)
//}

var HotelBookAjaxCall = (id) => {
    var server = "/api/hotel/book/" + id;
    $.ajax({
        url: server,
        type: 'PUT',
        contentType: 'application/json',
        dataType: 'json',
        success: function (result) {
            console.log("success");
            console.log(result);
        },
        error: function (reason) {
            console.log("had a failure");
            console.log(reason);
        }
    }).done((response) => {
        alert("Booked");
    });
};

var HotelSaveAjaxCall = (id) => {
    var server = "/api/hotel/save/" + id;
    $.ajax({
        url: server,
        type: 'PUT',
        contentType: 'application/json',
        dataType: 'json',
        success: function (result) {
            console.log("success");
            console.log(result);
        },
        error: function (reason) {
            console.log("had a failure");
            console.log(reason);
        }
    }).done((response) => {
        alert("saved");
    });
};

var GetAllHotels = () => {

    HotelAllAjaxCall()
}
var HotelAllAjaxCall = () => {
    var server = "/api/hotel/get/";
    $.ajax({
        url: server,
        type: 'GET',
        contentType: 'application/json',
        dataType: 'json',
        success: function (result) {
            console.log("success");
            console.log(result);
        },
        error: function (reason) {
            console.log("had a failure");
            console.log(reason);
        }
    }).done((response) => {
        if ($('#hotels').children().length == 0 ) {
            response.forEach((hotel) => {
                var book_id = "b" + hotel.Id;
                var save_id = "s" + hotel.Id;
                $("#hotels").append("<p>" + "<span>" + hotel.Name + "</span><span>" + hotel.Price + "</span><span>" + hotel.Address + "</span><span>" + hotel.StarRating + "</span><span>" + "<button class='btn' id=" + book_id + ">Book</button>" + "</span><span>" + "<button class='btn' id=" + save_id + ">Save</button>"+"</span>"+"</p>");
              
                document.getElementById(book_id).addEventListener("click", () => {
                    HotelBookAjaxCall(hotel.Id);
                });
                document.getElementById(save_id).addEventListener("click", () => {
                    HotelSaveAjaxCall(hotel.Id);
                });

            });
        }
    });
};
//Car
Car = {};
var addCar = () => {
    var name = $('#car_name').val();
    var start = $('#start').val();
    var destination = $('#destination').val();
    var pickup = $('#pickup').val();
    var drop = $('#drop').val();
    Car.Name = name;
    Car.Pick = start;
    Car.Drop = destination;
    Car.PickDate = pickup;
    Car.DropDate = drop;
    CarCall(Car);
}
var CarCall = (Car) => {
    $.ajax({
        url: "/api/car/add",
        data: JSON.stringify(Car),
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        async: false,
        success: function (result) {
            console.log("success");

        },
        error: function (reason) {
            console.log("had a failure");
            console.log(reason);
        }
    }).done((response) => { appendData(response) });
};
var GetCar = () => {
    var id = $("#hotelId").val();
    CarAjaxCall(id)
}

var CarAjaxCall = (id) => {
    var server = "/api/car/get/" + id;
    $.ajax({
        url: server,
        type: 'GET',
        contentType: 'application/json',
        dataType: 'json',
        success: function (result) {
            console.log("success");
            console.log(result);
        },
        error: function (reason) {
            console.log("had a failure");
            console.log(reason);
        }
    }).done((response) => {
        $("#cars").append("<p>" + response.Name + "</p>")
    });
};


var GetAllCars = () => {

    CarAllAjaxCall()
}
var CarBookAjaxCall = (id) => {
    var server = "/api/car/book/" + id;
    $.ajax({
        url: server,
        type: 'PUT',
        contentType: 'application/json',
        dataType: 'json',
        success: function (result) {
            console.log("success");
            console.log(result);
        },
        error: function (reason) {
            console.log("had a failure");
            console.log(reason);
        }
    }).done((response) => {
        alert("Booked");
    });
};

var CarSaveAjaxCall = (id) => {
    var server = "/api/car/save/" + id;
    $.ajax({
        url: server,
        type: 'PUT',
        contentType: 'application/json',
        dataType: 'json',
        success: function (result) {
            console.log("success");
            console.log(result);
        },
        error: function (reason) {
            console.log("had a failure");
            console.log(reason);
        }
    }).done((response) => {
        alert("saved");
    });
};
var CarAllAjaxCall = () => {
    var server = "/api/car/get/";
    $.ajax({
        url: server,
        type: 'GET',
        contentType: 'application/json',
        dataType: 'json',
        success: function (result) {
            console.log("success");
            console.log(result);
        },
        error: function (reason) {
            console.log("had a failure");
            console.log(reason);
        }
    }).done((response) => {
        if ($('#cars').children().length == 0) {
            response.forEach((car) => {
                var book_id = "b" + car.Id;
                var save_id = "s" + car.Id;
                $("#cars").append("<p>" + "<span>" + car.Drop + " </span >" + "<span>" + car.Pick + "</span> " + "<span>" + car.DropDate + "</span>" + "<span>" + car.PickDate + "</span><span>" + "<button class='btn' id=" + book_id + ">Book</button>" + "</span><span>" + "<button class='btn' id=" + save_id + ">Save</button>"+"</span>"+"</p>");
                document.getElementById(book_id).addEventListener("click", () => {
                    CarBookAjaxCall(car.Id);
                });
                document.getElementById(save_id).addEventListener("click", () => {
                    CarSaveAjaxCall(car.Id);
                });

            });
        }
    });
};
//Flight

Flight = {};
var addFlight = () => {
    var name = $('#flight_name').val();
    var from = $('#from').val();
    var to = $('#to').val();
    var arrival = $('#arrival').val();
    var departure = $('#departure').val();
    var cost = $('#cost').val();
    var duration = $('#time').val();
    Flight.Name = name;
    Flight.From = from;
    Flight.To = to;
    Flight.Arrival = arrival;
    Flight.Departure = departure;
    Flight.Cost = cost;
    Flight.time = duration;
    FlightCall(Flight);
}
var FlightCall = (Flight) => {
    $.ajax({
        url: "/api/flight/add",
        data: JSON.stringify(Flight),
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        async: false,
        success: function (result) {
            console.log("success");

        },
        error: function (reason) {
            console.log("had a failure");
            console.log(reason);
        }
    }).done((response) => { appendData(response) });
};
var FlightAjaxCall = (id) => {
    var server = "/api/flight/get/" + id;
    $.ajax({
        url: server,
        type: 'GET',
        contentType: 'application/json',
        dataType: 'json',
        success: function (result) {
            console.log("success");
            console.log(result);
        },
        error: function (reason) {
            console.log("had a failure");
            console.log(reason);
        }
    }).done((response) => {
        $("#cars").append("<p>" + response.Name + "</p>")
    });
};


var GetAllFlights = () => {

    FlightAllAjaxCall()
}
var FlightBookAjaxCall = (id) => {
    var server = "/api/flight/book/" + id;
    $.ajax({
        url: server,
        type: 'PUT',
        contentType: 'application/json',
        dataType: 'json',
        success: function (result) {
            console.log("success");
            console.log(result);
        },
        error: function (reason) {
            console.log("had a failure");
            console.log(reason);
        }
    }).done((response) => {
        alert("Booked Successfully");
    });
};

var FlightSaveAjaxCall = (id) => {
    var server = "/api/flight/save/" + id;
    $.ajax({
        url: server,
        type: 'PUT',
        contentType: 'application/json',
        dataType: 'json',
        success: function (result) {
            console.log("success");
            console.log(result);
        },
        error: function (reason) {
            console.log("had a failure");
            console.log(reason);
        }
    }).done((response) => {
        alert("saved Successfully");
    });
};
var FlightAllAjaxCall = () => {
    var server = "/api/flight/get/";
    $.ajax({
        url: server,
        type: 'GET',
        contentType: 'application/json',
        dataType: 'json',
        success: function (result) {
            console.log("success");
            console.log(result);
        },
        error: function (reason) {
            console.log("had a failure");
            console.log(reason);
        }
    }).done((response) => {
        if ($('#flights').children().length == 0) {
            response.forEach((flight) => {
                var book_id = "b" + flight.Id;
                var save_id = "s" + flight.Id;
                $("#flights").append("<p>" + "<span>" + flight.Name + "</span><span>" + flight.From + "</span><span>" + flight.To + "</span><span>" + flight.Cost + "</span><span>" + flight.time + "</span><span>" + "<button class='btn' id=" + book_id + ">Book</button>" + "</span><span>" + "<button class='btn' id=" + save_id + ">Save</button>"+"</span>"+"</p>");
               document.getElementById(book_id).addEventListener("click", () => {
                    FlightBookAjaxCall(flight.Id);
                });
                document.getElementById(save_id).addEventListener("click", () => {
                    FlightSaveAjaxCall(flight.Id);
                });

            });
        }
    });
};

//Activity

Activity = {};
var addActivity = () => {
    var name = $('#activity_name').val();
    var destination = $('#activity_destination').val();
    var start = $('#start-date').val();
    var end = $('#end-date').val();
    Activity.Name = name;
    Activity.Destination = destination;
    Activity.StartDate = start;
    Activity.LastDate = end;
    ActivityCall(Activity);
}
var ActivityCall = (Activity) => {
    $.ajax({
        url: "/api/activity/add",
        data: JSON.stringify(Activity),
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        async: false,
        success: function (result) {
            console.log("success");

        },
        error: function (reason) {
            console.log("had a failure");
            console.log(reason);
        }
    }).done((response) => { appendData(response) });
};
var ActivityAjaxCall = (id) => {
    var server = "/api/activity/get/" + id;
    $.ajax({
        url: server,
        type: 'GET',
        contentType: 'application/json',
        dataType: 'json',
        success: function (result) {
            console.log("success");
            console.log(result);
        },
        error: function (reason) {
            console.log("had a failure");
            console.log(reason);
        }
    }).done((response) => {
        $("#activity").append("<p>" + response.Name + "</p>")
    });
};


var GetAllActivities = () => {

    ActivityAllAjaxCall()
}
var ActivityBookAjaxCall = (id) => {
    var server = "/api/activity/book/" + id;
    $.ajax({
        url: server,
        type: 'PUT',
        contentType: 'application/json',
        dataType: 'json',
        success: function (result) {
            console.log("success");
            console.log(result);
        },
        error: function (reason) {
            console.log("had a failure");
            console.log(reason);
        }
    }).done((response) => {
        alert("Booked Successfully");
    });
};

var ActivitySaveAjaxCall = (id) => {
    var server = "/api/activity/save/" + id;
    $.ajax({
        url: server,
        type: 'PUT',
        contentType: 'application/json',
        dataType: 'json',
        success: function (result) {
            console.log("success");
            console.log(result);
        },
        error: function (reason) {
            console.log("had a failure");
            console.log(reason);
        }
    }).done((response) => {
        alert("saved successfully");
    });
};
var ActivityAllAjaxCall = () => {
    var server = "/api/activity/get/";
    $.ajax({
        url: server,
        type: 'GET',
        contentType: 'application/json',
        dataType: 'json',
        success: function (result) {
            console.log("success");
            console.log(result);
        },
        error: function (reason) {
            console.log("had a failure");
            console.log(reason);
        }
    }).done((response) => {
        if ($('#activity').children().length == 0) {
            response.forEach((activity) => {
                var book_id = "b" + activity.Id;
                var save_id = "s" + activity.Id;
                $("#activity").append("<p>" + "<span>" + activity.Name + "</span><span>" + activity.Destination + "</span><span>" + activity.StartDate + "</span><span>" + activity.LastDate + "</span><span>" + "<button class='btn' id=" + book_id + ">Book</button>" + "</span><span>"+"<button class='btn' id=" + save_id + ">Save</button>"+"</span>"+"</p>");
               document.getElementById(book_id).addEventListener("click", () => {
                    ActivityBookAjaxCall(activity.Id);
                });
                document.getElementById(save_id).addEventListener("click", () => {
                    ActivitySaveAjaxCall(activity.Id);
                });

            });
        }
    });
};