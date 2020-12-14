// Copyright 2020 Google Inc.

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
using OpenMatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Gamefrontend.GameFrontendMMService;

namespace Game_Frontend
{
    class GameFrontendService : GameFrontendMMServiceBase
    {
        public override Task<GameFrontendResponse> FindMatch(ClientRequest request, ServerCallContext context)
        {
            // TO DO:
            Console.WriteLine($"Matchmaking request received from player with id {request.Info.Id.ToString()}");
            // Access player information and generate ticket using player data
            // Submit ticket to Open Match and wait for assignment

            // Send assignment response to game client
            Assignment response = new Assignment() { Connection = GenerateConnAddr() }; // This is used for example purposes and GenerateConnAddr to create a dummy connection string

            Console.WriteLine($"Assigning the client the following address: {response.Connection}");

            return Task.FromResult(new GameFrontendResponse() { Assignment = response});
        }

        // This function is used to generate a dummy ip address to represent a connection string to a game server
        private string GenerateConnAddr()
        {
            Random r = new Random();
            return r.Next(0,256) + "." + r.Next(0, 256) + "." + r.Next(0, 256) + "." + r.Next(0, 256);
        }
    }
}
