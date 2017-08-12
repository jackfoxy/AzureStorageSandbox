namespace AzureStorageSandbox

open FSharp.Configuration
//open FSharp.Azure.StorageTypeProvider
open Microsoft.WindowsAzure.Storage.Queue
open Microsoft.WindowsAzure.Storage.RetryPolicies



module console1 =
    [<EntryPoint>]
    let main argv = 
        printfn "%A" argv

        
        //let queue = Azure.Queues.CloudQueueClient.GetQueueReference("test")

        //let queueRequestOptions = new QueueRequestOptions()
        //queue.CreateIfNotExists(queueRequestOptions) |> ignore

        //CloudQueueMessage(
        //queue.AddMessage

        //Blobs.insertBlob @"E:\BitSync\PersonalServer\KnuthThunderbird.csv" "thunderbird/knuth.csv"
        Blobs.insertBlob @"E:\BitSync\PersonalServer\LinkedInContacts.csv" "LinkedInContacts/LinkedInContacts.csv"


        
        printfn "Hit any key to exit."
        System.Console.ReadKey() |> ignore
        0
