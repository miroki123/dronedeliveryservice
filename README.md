# dronedeliveryservice
Velozient Challenge

How to run:
1. Clone the repo into a local folder
2. Open the project in Visual Studio
3. Edit the input.txt file inside the project root and add the input data you'd like to test
4. Build & Run the project 'DroneDeliveryService'.
5. If everything was setup correctly, a console will run and display the results

   ![image](https://github.com/miroki123/dronedeliveryservice/assets/11218839/fb29b345-ddb0-40be-af4f-f43606f530a6)

To solve for the problem of using the least amount of trips, I used a slightly modified version of the knapsack algorithm that allows me to return the items used in the code.

So we will iterate through a list beginning with all the locations. -- see method CalculateDeliveries(Squad squad, Delivery delivery)

In each location iteration, I will run the knapsack for each drone, and select the solution that returns me the highest amount of locations.

In the end of each iteration, I will remove the locations selected by the drone from the iterating locations list.

This runs until there are no more locations.
