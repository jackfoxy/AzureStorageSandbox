namespace AzureStorageSandbox

open System.IO

open Microsoft.WindowsAzure
open Microsoft.WindowsAzure.Storage
open Microsoft.WindowsAzure.Storage.Queue

module Queues =

    let queueClient = Common.storageAccount.CreateCloudQueueClient()
    let queue = queueClient.GetQueueReference("rawcontacts")

    queue.CreateIfNotExists() |> ignore

    let cloudQueueMessage = new CloudQueueMessage("message")
   

    ()

