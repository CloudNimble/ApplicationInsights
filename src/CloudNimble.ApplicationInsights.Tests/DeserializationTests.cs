using CloudNimble.ApplicationInsights.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace CloudNimble.ApplicationInsights.Tests
{

    [TestClass]
    public class DeserializationTests
    {

#pragma warning disable CS0414 // Remove unused private members

        private string exception = """
            [
              {
                "time": "2024-07-13T19:04:03.344Z",
                "iKey": "219f9af4-0842-42c8-a5b1-578f09d2ee27",
                "name": "Microsoft.ApplicationInsights.219f9af4084242c8a5b1578f09d2ee27.Exception",
                "tags": {
                  "ai.user.id": "bS3J3YEbF2uCz/+2j1h58c",
                  "ai.session.id": "N3MEcqj+ATILj6Mz14XIjM",
                  "ai.device.id": "browser",
                  "ai.device.type": "Browser",
                  "ai.operation.name": "/",
                  "ai.operation.id": "4a958dee7aba4184b28724f3e51766a8",
                  "ai.internal.sdkVersion": "javascript:3.2.2",
                  "ai.cloud.role": "SPA",
                  "ai.cloud.roleInstance": "Blazor Wasm"
                },
                "data": {
                  "baseType": "ExceptionData",
                  "baseData": {
                    "ver": 2,
                    "exceptions": [
                      {
                        "typeName": "my error",
                        "message": "my error: my message",
                        "hasFullStack": false,
                        "stack": "my message\n",
                        "parsedStack": []
                      }
                    ],
                    "severityLevel": 3,
                    "properties": {
                      "customProperty": "customValue",
                      "typeName": "my error"
                    }
                  }
                }
              }
            ]
            """;

        private string globalException = """
            [
              {
                "time": "2024-07-13T02:42:26.419Z",
                "iKey": "219f9af4-0842-42c8-a5b1-578f09d2ee27",
                "name": "Microsoft.ApplicationInsights.219f9af4084242c8a5b1578f09d2ee27.Exception",
                "tags": {
                  "ai.user.id": "bS3J3YEbF2uCz/+2j1h58c",
                  "ai.session.id": "pAu8sUJ956X3ixhZOKft0l",
                  "ai.device.id": "browser",
                  "ai.device.type": "Browser",
                  "ai.operation.name": "/",
                  "ai.operation.id": "733c774f66cb49b5aca3bede71b9816a",
                  "ai.internal.sdkVersion": "javascript:3.2.2",
                  "ai.cloud.role": "SPA",
                  "ai.cloud.roleInstance": "Blazor Wasm"
                },
                "data": {
                  "baseType": "ExceptionData",
                  "baseData": {
                    "ver": 2,
                    "exceptions": [
                      {
                        "typeName": "NotImplementedException",
                        "message": "NotImplementedException: Something wrong happened :(",
                        "hasFullStack": false,
                        "stack": "System.NotImplementedException: Something wrong happened :(\n ---> System.InvalidOperationException: TEST INNER\n   Exception_EndOfInnerExceptionStack\n   at BlazorApplicationInsights.Sample.Components.TestComponents.TrackGlobalException()\n   at Microsoft.AspNetCore.Components.EventCallbackWorkItem.InvokeAsync[Object](MulticastDelegate , Object )\n   at Microsoft.AspNetCore.Components.EventCallbackWorkItem.InvokeAsync(Object )\n   at Microsoft.AspNetCore.Components.ComponentBase.Microsoft.AspNetCore.Components.IHandleEvent.HandleEventAsync(EventCallbackWorkItem callback, Object arg)\n   at Microsoft.AspNetCore.Components.EventCallback.InvokeAsync(Object )\n   at Microsoft.AspNetCore.Components.RenderTree.Renderer.DispatchEventAsync(UInt64 , EventFieldInfo , EventArgs , Boolean )\n",
                        "parsedStack": []
                      }
                    ],
                    "severityLevel": 4,
                    "properties": {
                      "CategoryName": "Microsoft.AspNetCore.Components.WebAssembly.Rendering.WebAssemblyRenderer",
                      "EventId": "100",
                      "EventName": "ExceptionRenderingComponent",
                      "Message": "Something wrong happened :(",
                      "OriginalFormat": "Unhandled exception rendering component: {Message}",
                      "id": "ExceptionRenderingComponent",
                      "typeName": "NotImplementedException"
                    }
                  }
                }
              },
              {
                "time": "2024-07-13T02:42:33.582Z",
                "iKey": "219f9af4-0842-42c8-a5b1-578f09d2ee27",
                "name": "Microsoft.ApplicationInsights.219f9af4084242c8a5b1578f09d2ee27.Event",
                "tags": {
                  "ai.user.id": "bS3J3YEbF2uCz/+2j1h58c",
                  "ai.session.id": "pAu8sUJ956X3ixhZOKft0l",
                  "ai.device.id": "browser",
                  "ai.device.type": "Browser",
                  "ai.operation.name": "/",
                  "ai.operation.id": "733c774f66cb49b5aca3bede71b9816a",
                  "ai.internal.sdkVersion": "javascript:3.2.2",
                  "ai.cloud.role": "SPA",
                  "ai.cloud.roleInstance": "Blazor Wasm"
                },
                "data": {
                  "baseType": "EventData",
                  "baseData": {
                    "ver": 2,
                    "name": "My Event",
                    "properties": {
                      "customProperty": "customValue"
                    },
                    "measurements": {}
                  }
                }
              }
            ]
            """;

        private string messageData = """
            [
              {
                "time": "2024-07-13T02:42:35.718Z",
                "iKey": "219f9af4-0842-42c8-a5b1-578f09d2ee27",
                "name": "Microsoft.ApplicationInsights.219f9af4084242c8a5b1578f09d2ee27.Message",
                "tags": {
                  "ai.user.id": "bS3J3YEbF2uCz/+2j1h58c",
                  "ai.session.id": "pAu8sUJ956X3ixhZOKft0l",
                  "ai.device.id": "browser",
                  "ai.device.type": "Browser",
                  "ai.operation.name": "/",
                  "ai.operation.id": "733c774f66cb49b5aca3bede71b9816a",
                  "ai.internal.sdkVersion": "javascript:3.2.2",
                  "ai.internal.snippet": "7",
                  "ai.internal.sdkSrc": "https://js.monitor.azure.com/scripts/b/ai.3.gbl.min.js",
                  "ai.cloud.role": "SPA",
                  "ai.cloud.roleInstance": "Blazor Wasm"
                },
                "data": {
                  "baseType": "MessageData",
                  "baseData": {
                    "ver": 2,
                    "message": "myMessage",
                    "properties": {}
                  }
                }
              }
            ]
            """;

        private string messageDataSemanticLogger = """
            [
              {
                "time": "2024-07-13T06:38:24.449Z",
                "iKey": "219f9af4-0842-42c8-a5b1-578f09d2ee27",
                "name": "Microsoft.ApplicationInsights.219f9af4084242c8a5b1578f09d2ee27.Message",
                "tags": {
                  "ai.user.id": "bS3J3YEbF2uCz/+2j1h58c",
                  "ai.session.id": "t7Mm4FbHMMQZ7taqz73KO9",
                  "ai.device.id": "browser",
                  "ai.device.type": "Browser",
                  "ai.operation.name": "/",
                  "ai.operation.id": "9a8bb020511b4d57b10ced0ddff05064",
                  "ai.internal.sdkVersion": "javascript:3.2.2",
                  "ai.internal.snippet": "7",
                  "ai.internal.sdkSrc": "https://js.monitor.azure.com/scripts/b/ai.3.gbl.min.js",
                  "ai.cloud.role": "SPA",
                  "ai.cloud.roleInstance": "Blazor Wasm"
                },
                "data": {
                  "baseType": "MessageData",
                  "baseData": {
                    "ver": 2,
                    "message": "My Semantic Logging Test with customProperty=customValue",
                    "severityLevel": 1,
                    "properties": {
                      "CategoryName": "BlazorApplicationInsights.Sample.Components.TestComponents",
                      "customProperty": "customValue",
                      "OriginalFormat": "My Semantic Logging Test with customProperty={customProperty}"
                    }
                  }
                }
              }
            ]
            """;

        private string metricData = """
            [
              {
                "time": "2024-07-13T06:35:36.364Z",
                "iKey": "219f9af4-0842-42c8-a5b1-578f09d2ee27",
                "name": "Microsoft.ApplicationInsights.219f9af4084242c8a5b1578f09d2ee27.Metric",
                "tags": {
                  "ai.user.id": "bS3J3YEbF2uCz/+2j1h58c",
                  "ai.session.id": "t7Mm4FbHMMQZ7taqz73KO9",
                  "ai.device.id": "browser",
                  "ai.device.type": "Browser",
                  "ai.operation.name": "/",
                  "ai.operation.id": "9a8bb020511b4d57b10ced0ddff05064",
                  "ai.internal.sdkVersion": "javascript:3.2.2",
                  "ai.cloud.role": "SPA",
                  "ai.cloud.roleInstance": "Blazor Wasm"
                },
                "data": {
                  "baseType": "MetricData",
                  "baseData": {
                    "ver": 2,
                    "metrics": [
                      {
                        "name": "myMetric",
                        "kind": 0,
                        "value": 100,
                        "count": 200,
                        "min": 1,
                        "max": 200
                      }
                    ],
                    "properties": {
                      "customProperty": "customValue"
                    }
                  }
                }
              }
            ]
            """;

        private string pageviewData = """
            [
              {
                "time": "2024-07-13T02:43:37.440Z",
                "iKey": "219f9af4-0842-42c8-a5b1-578f09d2ee27",
                "name": "Microsoft.ApplicationInsights.219f9af4084242c8a5b1578f09d2ee27.Pageview",
                "tags": {
                  "ai.user.id": "bS3J3YEbF2uCz/+2j1h58c",
                  "ai.session.id": "pAu8sUJ956X3ixhZOKft0l",
                  "ai.device.id": "browser",
                  "ai.device.type": "Browser",
                  "ai.operation.name": "/",
                  "ai.operation.id": "733c774f66cb49b5aca3bede71b9816a",
                  "ai.internal.sdkVersion": "javascript:3.2.2",
                  "ai.internal.snippet": "7",
                  "ai.internal.sdkSrc": "https://js.monitor.azure.com/scripts/b/ai.3.gbl.min.js",
                  "ai.cloud.role": "SPA",
                  "ai.cloud.roleInstance": "Blazor Wasm"
                },
                "data": {
                  "baseType": "PageviewData",
                  "baseData": {
                    "ver": 2,
                    "name": "myPage",
                    "url": "https://test.local",
                    "duration": "00:00:00.959",
                    "properties": {
                      "customProperty": "customValue",
                      "refUri": "https://test.local",
                      "pageType": "TestPage",
                      "isLoggedIn": "true"
                    },
                    "measurements": {},
                    "id": "733c774f66cb49b5aca3bede71b9816a"
                  }
                }
              }
            ]
            """;

        private string pageviewPerformanceData = """
        [
          {
            "time": "2024-07-13T02:43:39.952Z",
            "iKey": "219f9af4-0842-42c8-a5b1-578f09d2ee27",
            "name": "Microsoft.ApplicationInsights.219f9af4084242c8a5b1578f09d2ee27.PageviewPerformance",
            "tags": {
              "ai.user.id": "bS3J3YEbF2uCz/+2j1h58c",
              "ai.session.id": "pAu8sUJ956X3ixhZOKft0l",
              "ai.device.id": "browser",
              "ai.device.type": "Browser",
              "ai.operation.name": "/",
              "ai.operation.id": "733c774f66cb49b5aca3bede71b9816a",
              "ai.internal.sdkVersion": "javascript:3.2.2",
              "ai.cloud.role": "SPA",
              "ai.cloud.roleInstance": "Blazor Wasm"
            },
            "data": {
              "baseType": "PageviewPerformanceData",
              "baseData": {
                "ver": 2,
                "name": "myPerf",
                "url": "/test123",
                "duration": "00:00:00.959",
                "perfTotal": "00:00:00.959",
                "networkConnect": "00:00:00.017",
                "sentRequest": "00:00:00.301",
                "receivedResponse": "00:00:00.052",
                "domProcessing": "00:00:00.568",
                "properties": {
                  "customProperty": "customValue"
                },
                "measurements": {}
              }
            }
          }
        ]
        """;

        private string remoteDependencyData = """
            [
              {
                "time": "2024-07-13T02:42:40.656Z",
                "iKey": "219f9af4-0842-42c8-a5b1-578f09d2ee27",
                "name": "Microsoft.ApplicationInsights.219f9af4084242c8a5b1578f09d2ee27.RemoteDependency",
                "tags": {
                  "ai.user.id": "bS3J3YEbF2uCz/+2j1h58c",
                  "ai.session.id": "pAu8sUJ956X3ixhZOKft0l",
                  "ai.device.id": "browser",
                  "ai.device.type": "Browser",
                  "ai.operation.name": "/",
                  "ai.operation.id": "733c774f66cb49b5aca3bede71b9816a",
                  "ai.internal.sdkVersion": "javascript:3.2.2",
                  "ai.cloud.role": "SPA",
                  "ai.cloud.roleInstance": "Blazor Wasm"
                },
                "data": {
                  "baseType": "RemoteDependencyData",
                  "baseData": {
                    "id": "myId.",
                    "ver": 2,
                    "name": "myName",
                    "resultCode": "200",
                    "duration": "00:00:01.000",
                    "success": true,
                    "data": "myName",
                    "target": "blazorapplicationinsights.netlify.app | myContext",
                    "type": "myType",
                    "properties": {
                      "customProperty": "customValue"
                    },
                    "measurements": {}
                  }
                }
              }
            ]
            """;

        public string remoteDependencyDataWeb = """
            [
              {
                "time": "2024-07-13T06:36:44.187Z",
                "iKey": "219f9af4-0842-42c8-a5b1-578f09d2ee27",
                "name": "Microsoft.ApplicationInsights.219f9af4084242c8a5b1578f09d2ee27.RemoteDependency",
                "tags": {
                  "ai.user.id": "bS3J3YEbF2uCz/+2j1h58c",
                  "ai.session.id": "t7Mm4FbHMMQZ7taqz73KO9",
                  "ai.device.id": "browser",
                  "ai.device.type": "Browser",
                  "ai.operation.name": "/",
                  "ai.operation.id": "9a8bb020511b4d57b10ced0ddff05064",
                  "ai.internal.sdkVersion": "javascript:3.2.2",
                  "ai.cloud.role": "SPA",
                  "ai.cloud.roleInstance": "Blazor Wasm"
                },
                "data": {
                  "baseType": "RemoteDependencyData",
                  "baseData": {
                    "id": "|9a8bb020511b4d57b10ced0ddff05064.853ae88e662941c5.",
                    "ver": 2,
                    "name": "GET https://httpbin.org/get",
                    "resultCode": "200",
                    "duration": "00:00:01.728",
                    "success": true,
                    "data": "GET https://httpbin.org/get",
                    "target": "httpbin.org",
                    "type": "Fetch",
                    "properties": {
                      "HttpMethod": "GET"
                    },
                    "measurements": {}
                  }
                }
              }
            ]
            """;

#pragma warning restore CS0141 // Remove unused private members


        [TestMethod]
        public void Exception_ShouldDeserialize()
        {
            var test = JsonSerializer.Deserialize<List<RequestDetails<ExceptionData>>>(exception, TelemetryClient.JsonSerializerOptions);
            test.Should().NotBeNullOrEmpty();
            test.Should().ContainSingle();
            test[0].Should().NotBeNull();
            test[0].InstrumentationKey.Should().Be("219f9af4-0842-42c8-a5b1-578f09d2ee27");
            test[0].Name.Should().Be("Microsoft.ApplicationInsights.219f9af4084242c8a5b1578f09d2ee27.Exception");
            test[0].Time.Should().Be(DateTimeOffset.Parse("2024-07-13T19:04:03.344Z"));
            test[0].Tags.Should().NotBeNullOrEmpty().And.HaveCount(9);
            test[0].Tags["ai.user.id"].Should().Be("bS3J3YEbF2uCz/+2j1h58c");
            test[0].Tags["ai.session.id"].Should().Be("N3MEcqj+ATILj6Mz14XIjM");
            test[0].Tags["ai.device.id"].Should().Be("browser");
            test[0].Tags["ai.device.type"].Should().Be("Browser");
            test[0].Tags["ai.operation.name"].Should().Be("/");
            test[0].Tags["ai.operation.id"].Should().Be("4a958dee7aba4184b28724f3e51766a8");
            test[0].Tags["ai.internal.sdkVersion"].Should().Be("javascript:3.2.2");
            test[0].Tags["ai.cloud.role"].Should().Be("SPA");
            test[0].Tags["ai.cloud.roleInstance"].Should().Be("Blazor Wasm");
            test[0].Data.Should().NotBeNull();
            test[0].Data.BaseType.Should().Be("ExceptionData");
            test[0].Data.BaseData.Should().NotBeNull();
            test[0].Data.BaseData.Exceptions.Should().NotBeNullOrEmpty().And.HaveCount(1);
            test[0].Data.BaseData.Exceptions[0].TypeName.Should().Be("my error");
            test[0].Data.BaseData.Exceptions[0].Message.Should().Be("my error: my message");
            test[0].Data.BaseData.Exceptions[0].HasFullStack.Should().BeFalse();
            test[0].Data.BaseData.Exceptions[0].Stack.Should().Be("my message\n");
            test[0].Data.BaseData.SeverityLevel.Should().Be(SeverityLevel.Error);
            test[0].Data.BaseData.Properties.Should().NotBeNullOrEmpty().And.HaveCount(2);
            test[0].Data.BaseData.Properties["customProperty"].Should().Be("customValue");
            test[0].Data.BaseData.Properties["typeName"].Should().Be("my error");
        }

        [TestMethod]
        public void GlobalException_ShouldDeserialize()
        {
            var test = JsonSerializer.Deserialize<List<RequestDetails<ExceptionData>>>(globalException, TelemetryClient.JsonSerializerOptions);
            test.Should().NotBeNullOrEmpty();
            test.Should().HaveCount(2);
            test[0].Should().NotBeNull();
            test[0].InstrumentationKey.Should().Be("219f9af4-0842-42c8-a5b1-578f09d2ee27");
            test[0].Name.Should().Be("Microsoft.ApplicationInsights.219f9af4084242c8a5b1578f09d2ee27.Exception");
            test[0].Time.Should().Be(DateTimeOffset.Parse("2024-07-13T02:42:26.419Z"));
            test[0].Tags.Should().NotBeNullOrEmpty().And.HaveCount(9);
            test[0].Tags["ai.user.id"].Should().Be("bS3J3YEbF2uCz/+2j1h58c");
            test[0].Tags["ai.session.id"].Should().Be("pAu8sUJ956X3ixhZOKft0l");
            test[0].Tags["ai.device.id"].Should().Be("browser");
            test[0].Tags["ai.device.type"].Should().Be("Browser");
            test[0].Tags["ai.operation.name"].Should().Be("/");
            test[0].Tags["ai.operation.id"].Should().Be("733c774f66cb49b5aca3bede71b9816a");
            test[0].Tags["ai.internal.sdkVersion"].Should().Be("javascript:3.2.2");
            test[0].Tags["ai.cloud.role"].Should().Be("SPA");
            test[0].Tags["ai.cloud.roleInstance"].Should().Be("Blazor Wasm");
            test[0].Data.Should().NotBeNull();
            test[0].Data.BaseType.Should().Be("ExceptionData");
            test[0].Data.BaseData.Should().NotBeNull();
            test[0].Data.BaseData.Exceptions.Should().NotBeNullOrEmpty().And.HaveCount(1);
            test[0].Data.BaseData.Exceptions[0].TypeName.Should().Be("NotImplementedException");
            test[0].Data.BaseData.Exceptions[0].Message.Should().Be("NotImplementedException: Something wrong happened :(");
            test[0].Data.BaseData.Exceptions[0].HasFullStack.Should().BeFalse();
            test[0].Data.BaseData.Exceptions[0].Stack.Should().Be("System.NotImplementedException: Something wrong happened :(\n ---> System.InvalidOperationException: TEST INNER\n   Exception_EndOfInnerExceptionStack\n   at BlazorApplicationInsights.Sample.Components.TestComponents.TrackGlobalException()\n   at Microsoft.AspNetCore.Components.EventCallbackWorkItem.InvokeAsync[Object](MulticastDelegate , Object )\n   at Microsoft.AspNetCore.Components.EventCallbackWorkItem.InvokeAsync(Object )\n   at Microsoft.AspNetCore.Components.ComponentBase.Microsoft.AspNetCore.Components.IHandleEvent.HandleEventAsync(EventCallbackWorkItem callback, Object arg)\n   at Microsoft.AspNetCore.Components.EventCallback.InvokeAsync(Object )\n   at Microsoft.AspNetCore.Components.RenderTree.Renderer.DispatchEventAsync(UInt64 , EventFieldInfo , EventArgs , Boolean )\n");
            //test[0].Data.BaseData.Exceptions[0].ParsedStack.Should().NotBeNullOrEmpty().And.BeEmpty();
            test[0].Data.BaseData.SeverityLevel.Should().Be(SeverityLevel.Critical);
            test[0].Data.BaseData.Properties.Should().NotBeNullOrEmpty().And.HaveCount(7);
            test[0].Data.BaseData.Properties["CategoryName"].Should().Be("Microsoft.AspNetCore.Components.WebAssembly.Rendering.WebAssemblyRenderer");
            test[0].Data.BaseData.Properties["EventId"].Should().Be("100");
            test[0].Data.BaseData.Properties["EventName"].Should().Be("ExceptionRenderingComponent");
            test[0].Data.BaseData.Properties["Message"].Should().Be("Something wrong happened :(");
            test[0].Data.BaseData.Properties["OriginalFormat"].Should().Be("Unhandled exception rendering component: {Message}");
            test[0].Data.BaseData.Properties["id"].Should().Be("ExceptionRenderingComponent");
            test[0].Data.BaseData.Properties["typeName"].Should().Be("NotImplementedException");
            test[1].Should().NotBeNull();
            test[1].InstrumentationKey.Should().Be("219f9af4-0842-42c8-a5b1-578f09d2ee27");
            test[1].Name.Should().Be("Microsoft.ApplicationInsights.219f9af4084242c8a5b1578f09d2ee27.Event");
            test[1].Time.Should().Be(DateTimeOffset.Parse("2024-07-13T02:42:33.582Z"));
            test[1].Tags.Should().NotBeNullOrEmpty().And.HaveCount(9);
            test[1].Tags["ai.user.id"].Should().Be("bS3J3YEbF2uCz/+2j1h58c");
            test[1].Tags["ai.session.id"].Should().Be("pAu8sUJ956X3ixhZOKft0l");
            test[1].Tags["ai.device.id"].Should().Be("browser");
            test[1].Tags["ai.device.type"].Should().Be("Browser");
            test[1].Tags["ai.operation.name"].Should().Be("/");
            test[1].Tags["ai.operation.id"].Should().Be("733c774f66cb49b5aca3bede71b9816a");
            test[1].Tags["ai.internal.sdkVersion"].Should().Be("javascript:3.2.2");
            test[1].Tags["ai.cloud.role"].Should().Be("SPA");
            test[1].Tags["ai.cloud.roleInstance"].Should().Be("Blazor Wasm");
            test[1].Data.Should().NotBeNull();
            test[1].Data.BaseType.Should().Be("EventData");
            test[1].Data.BaseData.Should().NotBeNull();
            test[1].Data.BaseData.Name.Should().Be("My Event");
            test[1].Data.BaseData.Properties.Should().NotBeNullOrEmpty().And.HaveCount(1);
            test[1].Data.BaseData.Properties["customProperty"].Should().Be("customValue");
            test[1].Data.BaseData.Measurements.Should().NotBeNull().And.BeEmpty();
        }

        [TestMethod]
        public void PageviewData_ShouldDeserialize()
        {
            var test = JsonSerializer.Deserialize<List<RequestDetails<PageviewData>>>(pageviewData, TelemetryClient.JsonSerializerOptions);
            test.Should().NotBeNullOrEmpty();
            test.Should().ContainSingle();
            test[0].Should().NotBeNull();
            test[0].InstrumentationKey.Should().Be("219f9af4-0842-42c8-a5b1-578f09d2ee27");
            test[0].Name.Should().Be("Microsoft.ApplicationInsights.219f9af4084242c8a5b1578f09d2ee27.Pageview");
            test[0].Time.Should().Be(DateTimeOffset.Parse("2024-07-13T02:43:37.440Z"));
            test[0].Tags.Should().NotBeNullOrEmpty().And.HaveCount(11);
            test[0].Tags["ai.user.id"].Should().Be("bS3J3YEbF2uCz/+2j1h58c");
            test[0].Tags["ai.session.id"].Should().Be("pAu8sUJ956X3ixhZOKft0l");
            test[0].Tags["ai.device.id"].Should().Be("browser");
            test[0].Tags["ai.device.type"].Should().Be("Browser");
            test[0].Tags["ai.operation.name"].Should().Be("/");
            test[0].Tags["ai.operation.id"].Should().Be("733c774f66cb49b5aca3bede71b9816a");
            test[0].Tags["ai.internal.sdkVersion"].Should().Be("javascript:3.2.2");
            test[0].Tags["ai.internal.snippet"].Should().Be("7");
            test[0].Tags["ai.internal.sdkSrc"].Should().Be("https://js.monitor.azure.com/scripts/b/ai.3.gbl.min.js");
            test[0].Tags["ai.cloud.role"].Should().Be("SPA");
            test[0].Tags["ai.cloud.roleInstance"].Should().Be("Blazor Wasm");
            test[0].Data.Should().NotBeNull();
            test[0].Data.BaseType.Should().Be("PageviewData");
            test[0].Data.BaseData.Should().NotBeNull();
            test[0].Data.BaseData.Name.Should().Be("myPage");
            test[0].Data.BaseData.Url.Should().Be("https://test.local");
            test[0].Data.BaseData.Duration.Should().Be(TimeOnly.Parse("00:00:00.959"));
            test[0].Data.BaseData.Properties.Should().NotBeNullOrEmpty().And.HaveCount(4);
            test[0].Data.BaseData.Properties["customProperty"].Should().Be("customValue");
            test[0].Data.BaseData.Properties["refUri"].Should().Be("https://test.local");
            test[0].Data.BaseData.Properties["pageType"].Should().Be("TestPage");
            test[0].Data.BaseData.Properties["isLoggedIn"].Should().Be("true");
            test[0].Data.BaseData.Measurements.Should().NotBeNull().And.BeEmpty();
            test[0].Data.BaseData.Id.Should().Be("733c774f66cb49b5aca3bede71b9816a");
        }

        [TestMethod]
        public void PageviewPerformanceData_ShouldDeserialize()
        {
            var test = JsonSerializer.Deserialize<List<RequestDetails<PageviewPerformanceData>>>(pageviewPerformanceData, TelemetryClient.JsonSerializerOptions);
            test.Should().NotBeNullOrEmpty();
            test.Should().ContainSingle();
            test[0].Should().NotBeNull();
            test[0].InstrumentationKey.Should().Be("219f9af4-0842-42c8-a5b1-578f09d2ee27");
            test[0].Name.Should().Be("Microsoft.ApplicationInsights.219f9af4084242c8a5b1578f09d2ee27.PageviewPerformance");
            test[0].Time.Should().Be(DateTimeOffset.Parse("2024-07-13T02:43:39.952Z"));
            test[0].Tags.Should().NotBeNullOrEmpty().And.HaveCount(9);
            test[0].Tags["ai.user.id"].Should().Be("bS3J3YEbF2uCz/+2j1h58c");
            test[0].Tags["ai.session.id"].Should().Be("pAu8sUJ956X3ixhZOKft0l");
            test[0].Tags["ai.device.id"].Should().Be("browser");
            test[0].Tags["ai.device.type"].Should().Be("Browser");
            test[0].Tags["ai.operation.name"].Should().Be("/");
            test[0].Tags["ai.operation.id"].Should().Be("733c774f66cb49b5aca3bede71b9816a");
            test[0].Tags["ai.internal.sdkVersion"].Should().Be("javascript:3.2.2");
            test[0].Tags["ai.cloud.role"].Should().Be("SPA");
            test[0].Tags["ai.cloud.roleInstance"].Should().Be("Blazor Wasm");
            test[0].Data.Should().NotBeNull();
            test[0].Data.BaseType.Should().Be("PageviewPerformanceData");
            test[0].Data.BaseData.Should().NotBeNull();
            test[0].Data.BaseData.Name.Should().Be("myPerf");
            test[0].Data.BaseData.Url.Should().Be("/test123");
            test[0].Data.BaseData.Duration.Should().Be(TimeOnly.Parse("00:00:00.959"));
            test[0].Data.BaseData.PerfTotal.Should().Be(TimeOnly.Parse("00:00:00.959"));
            test[0].Data.BaseData.NetworkConnect.Should().Be(TimeOnly.Parse("00:00:00.017"));
            test[0].Data.BaseData.SentRequest.Should().Be(TimeOnly.Parse("00:00:00.301"));
            test[0].Data.BaseData.ReceivedResponse.Should().Be(TimeOnly.Parse("00:00:00.052"));
            test[0].Data.BaseData.DomProcessing.Should().Be(TimeOnly.Parse("00:00:00.568"));
            test[0].Data.BaseData.Properties.Should().NotBeNullOrEmpty().And.HaveCount(1);
            test[0].Data.BaseData.Properties["customProperty"].Should().Be("customValue");
            test[0].Data.BaseData.Measurements.Should().NotBeNull().And.BeEmpty();
        }

        [TestMethod]
        public void RemoteDependencyData_Web_ShouldDeserialize()
        {
            var test = JsonSerializer.Deserialize<List<RequestDetails<RemoteDependencyData>>>(remoteDependencyDataWeb, TelemetryClient.JsonSerializerOptions);
            test.Should().NotBeNullOrEmpty();
            test.Should().ContainSingle();
            test[0].Should().NotBeNull();
            test[0].InstrumentationKey.Should().Be("219f9af4-0842-42c8-a5b1-578f09d2ee27");
            test[0].Name.Should().Be("Microsoft.ApplicationInsights.219f9af4084242c8a5b1578f09d2ee27.RemoteDependency");
            test[0].Time.Should().Be(DateTimeOffset.Parse("2024-07-13T06:36:44.187Z"));
            test[0].Tags.Should().NotBeNullOrEmpty().And.HaveCount(9);
            test[0].Tags["ai.user.id"].Should().Be("bS3J3YEbF2uCz/+2j1h58c");
            test[0].Tags["ai.session.id"].Should().Be("t7Mm4FbHMMQZ7taqz73KO9");
            test[0].Tags["ai.device.id"].Should().Be("browser");
            test[0].Tags["ai.device.type"].Should().Be("Browser");
            test[0].Tags["ai.operation.name"].Should().Be("/");
            test[0].Tags["ai.operation.id"].Should().Be("9a8bb020511b4d57b10ced0ddff05064");
            test[0].Tags["ai.internal.sdkVersion"].Should().Be("javascript:3.2.2");
            test[0].Tags["ai.cloud.role"].Should().Be("SPA");
            test[0].Tags["ai.cloud.roleInstance"].Should().Be("Blazor Wasm");
            test[0].Data.Should().NotBeNull();
            test[0].Data.BaseType.Should().Be("RemoteDependencyData");
            test[0].Data.BaseData.Should().NotBeNull();
            test[0].Data.BaseData.Id.Should().Be("|9a8bb020511b4d57b10ced0ddff05064.853ae88e662941c5.");
            test[0].Data.BaseData.Name.Should().Be("GET https://httpbin.org/get");
            test[0].Data.BaseData.ResultCode.Should().Be("200");
            test[0].Data.BaseData.Duration.Should().Be(TimeOnly.Parse("00:00:01.728"));
            test[0].Data.BaseData.Success.Should().BeTrue();
            test[0].Data.BaseData.Data.Should().Be("GET https://httpbin.org/get");
            test[0].Data.BaseData.Target.Should().Be("httpbin.org");
            test[0].Data.BaseData.Type.Should().Be("Fetch");
            test[0].Data.BaseData.Properties.Should().NotBeNullOrEmpty().And.HaveCount(1);
            test[0].Data.BaseData.Properties["HttpMethod"].Should().Be("GET");
            test[0].Data.BaseData.Measurements.Should().NotBeNull().And.BeEmpty();


        }

    }

}
