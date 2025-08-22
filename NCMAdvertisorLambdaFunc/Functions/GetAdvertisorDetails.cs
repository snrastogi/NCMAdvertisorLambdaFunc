using Amazon.Lambda.Annotations;
using Amazon.Lambda.Core;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using NCMAdvertisorLambdaFunc.Dto;
using NCMAdvertisorLambdaFunc.Models;
using NCMAdvertisorLambdaFunc.Services.Interface;
using Newtonsoft.Json;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace NCMAdvertisorLambdaFunc.Functions
{
    public class GetAdvertisorDetails
    {
        private readonly IMapper _mapper;
        private readonly IAdvertiserService _service;

        public GetAdvertisorDetails()
        {
            // Resolve dependencies from the static DI container
            _mapper = Program.Services.GetRequiredService<IMapper>();
            _service = Program.Services.GetRequiredService<IAdvertiserService>();
        }

        [LambdaFunction]
        public string FunctionHandler(string req, ILambdaContext context)
        {
            try
            {
                //var consumerConfig = new ConsumerConfig
                //{
                //    BootstrapServers = "localhost:9092",
                //    GroupId = "lambda-consumer-group",
                //    AutoOffsetReset = AutoOffsetReset.Earliest
                //};

                //string kafkaMessageValue = null;

                //try
                //{
                //    using (var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build())
                //    {
                //        consumer.Subscribe("advertisor-topic");
                //        var consumeResult = consumer.Consume(TimeSpan.FromSeconds(5));
                //        kafkaMessageValue = consumeResult?.Message?.Value;
                //    }
                //}
                //catch (Exception kafkaEx)
                //{
                //    context?.Logger.LogLine($"Kafka error: {kafkaEx}");
                //    return $"Error: Kafka consumption failed. {kafkaEx.Message}";
                //}

                //if (string.IsNullOrEmpty(kafkaMessageValue))
                //{
                //    context?.Logger.LogLine("No message received from Kafka.");
                //    return "No message received from Kafka.";
                //
                var kafkaMessageValue = req; // For testing purposes, use the input directly
                
                KafkaMessage kafkaMessage;
                try
                {

                    kafkaMessage = JsonConvert.DeserializeObject<KafkaMessage>(kafkaMessageValue);
                }
                catch (Exception jsonEx)
                {
                    context?.Logger.LogLine($"Deserialization error: {jsonEx}");
                    return $"Error: Failed to deserialize Kafka message. {jsonEx.Message}";
                }

                AdvertisorEntity entity;
                try
                {
                    entity = _mapper.Map<AdvertisorEntity>(kafkaMessage);
                }
                catch (Exception mapEx)
                {
                    context?.Logger.LogLine($"Mapping error: {mapEx}");
                    return $"Error: Failed to map Kafka message to entity. {mapEx.Message}";
                }

                try
                {
                    _service.ProcessAdvertisor(entity);
                }
                catch (Exception serviceEx)
                {
                    context?.Logger.LogLine($"Service error: {serviceEx}");
                    return $"Error: Service processing failed. {serviceEx.Message}";
                }

                context?.Logger.LogLine($"Successfully processed Advertisor Id: {entity.Id}");
                return $"Processed Advertisor Id: {entity.Id}";
            }
            catch (Exception ex)
            {
                context?.Logger.LogLine($"Unhandled error: {ex}");
                return $"Error: Unhandled exception. {ex.Message}";
            }
        }
    }
}