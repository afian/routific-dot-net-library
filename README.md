# Usage
```javascript
using Routific.Client;
using Routific;
using Newtonsoft.Json;
```

# Initialization

The Routific `Client` constructor does not need any arguments.


# Operations
Login
Authentication is done via username and password. There is no need to add an API key.

```javascript
var login = client.Login("test@test.com", "123456");
```

```javascript
VRP
VRPObject output = client.Route(vrp);
PDP :
PDPObject pdp_output = client.RoutePDP(pdp);

```

## VRP

This calls the /vrp-long endpoint and waits until the job is processed.

```javascript
			VRP vrp = new VRP();

            /*Add Visits */
            var visit = new Visits()
            {
                Location = new Location
                {
                    Name = "6800 Cambie",
                    Lat = 49.227107,
                    Lng = -123.1163085,
                },
                Start = "8:00",
                End = "16:00",
                Duration = 10,
            };
            vrp.addVisit("order_1", visit);

            var visit2 = new Visits()
            {
                Location = new Location
                {
                    Name = "6800 Cambie",
                    Lat = 49.227107,
                    Lng = -123.1163085,
                },
                Start = "9:00",
                End = "16:00",
                Duration = 10,
            };
            vrp.addVisit("order_2", visit2);

            /*Add Vehicle */
            var vehicle = new Vehicle()
            {
                StartLocation = new Location
                {
                    Id = "depot",
                    Lat = 49.2553636,
                    Lng = -123.0873365,
                },
                EndLocation = new Location
                {
                    Id = "depot",
                    Lat = 49.2553636,
                    Lng = -123.0873365,
                },

            };
            vrp.addVehicle("vehicle_1", vehicle);

            /*Add Option */
            vrp.addOption("traffic", "slow");

			var vrp_output_long = client.Long_Route_VRP(vrp);

```

## PDP route

This calls the /pdp-long endpoint and waits until the job is processed.

```javascript

			PDP pdp = new PDP();

            var pdp_order1 = new VisitsPDP()
            {
                Load = 1,
                Pickup = new Pickup
                {
                    Location = new LocationPDP
                    {
                        Name = "3780 Arbutus",
                        Lat = 49.2474624,
                        Lng = -123.1532338,
                    },
                    Start = "9:00",
                    End = "12:00",
                    Duration = 10,
                },

                Dropoff = new Dropoff
                {
                    Location = new LocationPDP
                    {
                        Name = "6800 Cambie",
                        Lat = 49.227107,
                        Lng = -123.1163085,
                    },
                    Start = "9:00",
                    End = "12:00",
                    Duration = 10,
                },

            };
            pdp.addVisitPDP("pdp_order1", pdp_order1);

            var pdp_order2 = new VisitsPDP()
            {
                Load = 1,
                Pickup = new Pickup
                {
                    Location = new LocationPDP
                    {
                        Name = "3780 Arbutus",
                        Lat = 49.2474624,
                        Lng = -123.1532338,
                    },
                    Start = "9:00",
                    End = "12:00",
                    Duration = 10,
                },

                Dropoff = new Dropoff
                {
                    Location = new LocationPDP
                    {
                        Name = "800 Robson",
                        Lat = 49.2819229,
                        Lng = -123.1211844,
                    },
                    Start = "9:00",
                    End = "12:00",
                    Duration = 10,
                },

            };
            pdp.addVisitPDP("pdp_order2", pdp_order2);

            /*Add Vehicle */
            var pdp_vehicle_1 = new VehiclePDP()
            {
                StartLocation = new LocationPDP
                {
                    Id = "depot",
                    Name = "800 Kingsway",
                    Lat = 49.2553636,
                    Lng = -123.0873365,
                },
                EndLocation = new LocationPDP
                {
                    Id = "depot",
                    Name = "800 Kingsway",
                    Lat = 49.2553636,
                    Lng = -123.0873365,
                },
                ShiftStart = "8:00",
                ShiftEnd = "12:00",
                Capacity =  2

            };
            pdp.addVehiclePDP("pdp_vehicle_1", pdp_vehicle_1);

            var pdp_vehicle_2 = new VehiclePDP()
            {
                StartLocation = new LocationPDP
                {
                    Id = "depot 2",
                    Name = "800 Robson",
                    Lat = 49.2553636,
                    Lng = -123.1211844,
                },
                EndLocation = new LocationPDP
                {
                    Id = "depot",
                    Name = "800 Kingsway",
                    Lat = 49.2553636,
                    Lng = -123.0873365,
                },
                ShiftStart = "8:00",
                ShiftEnd = "12:00",
                Capacity = 1

            };
            pdp.addVehiclePDP("pdp_vehicle_2", pdp_vehicle_2);

			var pdp_output_long = client.Long_Route_PDP(pdp);

```
