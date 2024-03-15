using System;
using System.Collections.Generic;
using System.Threading;

class Program {
    static void Main(string[] args) {
        List < string > busStops = new List < string > () {
            "Main Street",
            "Oak Avenue",
            "Maple Street",
            "Elm Road",
            "Pine Lane",
            "Cedar Boulevard",
            "Birch Way",
            "Walnut Street",
            "Aspen Court",
            "Cherry Drive"
        };

        BusTerminal terminal = new BusTerminal(maxCapacity: 20, busStops: busStops);
        terminal.BeginSimulation();
    }
}

internal class BusTerminal {
    private int maxCapacity;
    private int passengersAtStop;
    private int passengersOnBus;
    private bool busArriving;
    private readonly object terminalLock = new object();
    private List < string > busStops;

    public BusTerminal(int maxCapacity, List < string > busStops) {
        this.maxCapacity = maxCapacity;
        passengersAtStop = 0;
        passengersOnBus = 0;
        busArriving = false;
        this.busStops = busStops;
    }

    public void BeginSimulation() {
        Random randomGenerator = new Random();
        int stopIndex = 0;

        while (true) {
            int arrivingPassengers = randomGenerator.Next(1, 10);

            AnnounceBusArrival();
            Thread.Sleep(5000);
            DisembarkPassengers(busStops[stopIndex]);
            stopIndex = (stopIndex + 1) % busStops.Count;

            if (stopIndex == 0) {
                EmptyBusAtFinalStop();
                Console.WriteLine("Simulation complete: All bus stops visited.");
                break;
            }

            lock(terminalLock) {
                passengersOnBus += arrivingPassengers;
                Console.WriteLine($"Passengers arrived at stop: {arrivingPassengers}. Current passengers on bus: {passengersOnBus}");
            }
        }
    }

    public void AnnounceBusArrival() {
        lock(terminalLock) {
            if (!busArriving) {
                busArriving = true;
                Thread passengerBoardingThread = new Thread(BoardPassengers);
                passengerBoardingThread.Start();
            }
        }
    }

    public void BoardPassengers() {
        Console.WriteLine("Bus has arrived.");

        lock(terminalLock) {
            int seatsAvailable = maxCapacity - passengersOnBus;
            int boardingPassengers = Math.Min(passengersAtStop, seatsAvailable);
            passengersOnBus += boardingPassengers;
            passengersAtStop -= boardingPassengers;

            Console.WriteLine($"Bus departing with {boardingPassengers} new passengers. Passengers now on bus: {passengersOnBus}\n");
            busArriving = false;
        }
    }

    public void DisembarkPassengers(string stop) {
        Random randomGenerator = new Random();

        lock(terminalLock) {
            int disembarkingPassengers = randomGenerator.Next(0, passengersOnBus + 1);
            passengersOnBus -= disembarkingPassengers;
            Console.WriteLine($"{disembarkingPassengers} passengers disembarked at {stop}. Passengers remaining on bus: {passengersOnBus}");
        }
    }

    public void EmptyBusAtFinalStop() {
        lock(terminalLock) {
            Console.WriteLine($"Final stop: All {passengersOnBus} passengers have disembarked.");
            passengersOnBus = 0;
        }
    }
}