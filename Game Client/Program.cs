// Copyright 2020 Google LLC

// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at

//    http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Gamefrontend;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Client
{
    class Program
    {
        const string target = "127.0.0.1:50051";

        static void Main(string[] args)
        {
            Channel channel = new Channel(target, ChannelCredentials.Insecure);

            channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                Console.WriteLine("The client connected successfully");

            });

            var client = new GameFrontendMMService.GameFrontendMMServiceClient(channel);

            var info = new ClientInfo()
            {

                Id = GeneratePlayerId()

            };

            // TO DO
            // Add additonal information for request such as latency, region, etc


            var request = new ClientMatchMakingRequest() { Info = info };

            var response = client.FindMatch(request);

            Console.WriteLine($"This client received an assignment to the server located at {response.Assignment.Connection}");


            // TO DO
            // Connect to Game Server with assignment/address received

            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }

        static private Int32 GeneratePlayerId()
        {
            Random rnd = new Random();
            int playerId = rnd.Next(00000001, 10000000);

            return playerId;
        }
    }
}