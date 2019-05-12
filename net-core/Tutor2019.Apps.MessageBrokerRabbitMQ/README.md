cd .\bin\Debug\netcoreapp2.2

### Sample 01: https://www.rabbitmq.com/tutorials/tutorial-one-dotnet.html

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-01-receive

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-01-send

### Sample 02: https://www.rabbitmq.com/tutorials/tutorial-two-dotnet.html

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-02-worker

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-02-worker

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-02-new-task "Message 01."

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-02-new-task "Message 02.."

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-02-new-task "Message 03..."

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-02-new-task "Message 04...."

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-02-new-task "Message 05....."

### Sample 03: https://www.rabbitmq.com/tutorials/tutorial-three-dotnet.html

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-03-receive-logs-fanout > sample-03.log

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-03-receive-logs-fanout

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-03-emit-log-fanout "info: Message 01"

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-03-emit-log-fanout "warn: Message 02"

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-03-emit-log-fanout "error: Message 03"

### Sample 04: https://www.rabbitmq.com/tutorials/tutorial-four-dotnet.html

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-04-receive-logs-direct -ss 2 -ss 3 > sample-04.log

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-04-receive-logs-direct -ss 1 -ss 2 -ss 3

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-04-emit-log-direct "Message 01" -s 1

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-04-emit-log-direct "Message 02" -s 2

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-04-emit-log-direct "Message 03" -s 3

### Sample 05: https://www.rabbitmq.com/tutorials/tutorial-five-dotnet.html

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-05-receive-logs-topic -rr "#"

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-05-receive-logs-topic -rr "kern.*"

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-05-receive-logs-topic -rr "*.critical"

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-05-receive-logs-topic -rr "kern.*" -rr "*.critical"

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-05-emit-log-topic "Message 01" -r "kern.low"

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-05-emit-log-topic "Message 02" -r "kern.critical"

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-05-emit-log-topic "Message 03" -r "cron.low"

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-05-emit-log-topic "Message 04" -r "cron.critical"

### Sample 06: https://www.rabbitmq.com/tutorials/tutorial-six-dotnet.html

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-06-server

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-06-server

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-06-client 25

dotnet Tutor2019.Apps.MessageBrokerRabbitMQ.dll sample-06-client 35