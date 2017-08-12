namespace AzureStorageSandbox

open System.IO

open Microsoft.WindowsAzure
open Microsoft.WindowsAzure.Storage
open Microsoft.WindowsAzure.Storage.Blob

module Blobs =
    let insertBlob inPath outPath =
        //let container = Azure.Containers.CloudBlobClient.GetContainerReference("contactinput")
        //container.CreateIfNotExists() |> ignore
        //let x = CloudConfigurationManager.GetSetting("StorageConnectionString")
        let blobClient = Common.storageAccount.CreateCloudBlobClient()
        let container = blobClient.GetContainerReference("contactinput")
       
        container.CreateIfNotExists() 
//        container.CreateIfNotExistsAsync() 
//        |> Async.AwaitTask
//        |> Async.RunSynchronously
        |> ignore
        

        //Microsoft.WindowsAzure.Storage.Blob. CloudBlobContainer(
        
        use fileStream = System.IO.File.OpenRead(inPath)

        let blob = container.GetBlockBlobReference(outPath)

        blob.UploadFromStreamAsync(fileStream)
        |> Async.AwaitTask
        |> Async.RunSynchronously
        

