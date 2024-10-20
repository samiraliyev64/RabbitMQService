# Project Name: RabbitMQ Service

## Key Features:

### 1) Technologies Used:
 #### • .NET Web API for building the service.
 #### • RabbitMQ for message brokering and event handling.

### 2) Core Components:
 #### • EventBusBase: Manages event publishing and subscription.
 #### • Event Bus Factory: Simplifies the creation and configuration of event bus instances.
 #### • Subscription Manager: Handles subscription information and event routing.

### 3) Data Models:
 #### • Subscription Info: Contains details about event subscribers.
 #### • Bus Info: Provides information about the message bus configuration.
 #### • Integration Event: Represents events that are shared across different services.
 #### • Bus Event: Encapsulates events published on the event bus.

### 4) Connection Management:
 #### • RabbitMQ Persistent Connection: Ensures reliable connections to RabbitMQ.

### 5) Error Handling:
 #### • I implemented policies for handling socket exceptions to maintain service reliability.
