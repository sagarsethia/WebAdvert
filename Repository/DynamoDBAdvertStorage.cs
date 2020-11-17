using System.Threading.Tasks;
using AutoMapper;
using Amazon.DynamoDBv2;
using WebAdvertApi.Contract;
using WebAdvertApi.Model;
using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;

namespace WebAdvertApi.Repository
{
    public class DynamoDBAdvertStorage : IAdvertStorage
    {
        public readonly IMapper _mapper;
        public DynamoDBAdvertStorage(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<string> Add(AdvertModel model)
        {
            var dbModel = _mapper.Map<AdvertDBModel>(model);
            dbModel.Id = new Guid().ToString();
            dbModel.CreationDateTime = DateTime.UtcNow;
            dbModel.Status = AdvertStatus.pending;
            using (var client = new AmazonDynamoDBClient()){
                using(var context = new DynamoDBContext(client)){
                   await context.SaveAsync(dbModel);
                }
            }
            return dbModel.Id;
        }

        public async Task<bool> CheckHealthAsync()
        {
            using (var client = new AmazonDynamoDBClient())
            {
                var table = await client.DescribeTableAsync("Advert");
                return string.Compare(table.Table.TableStatus, "active", true) == 0;
            }
        }

        public async Task Confirm(ConfirmAdvertModel model)
        {
            using( var client = new AmazonDynamoDBClient()){
                using( var context = new DynamoDBContext(client)){
                   var record = await context.LoadAsync<AdvertDBModel>(model.Id);
                   if(record==null){
                       throw new KeyNotFoundException("Record With"+model.Id+ "Not found");
                   }
                   if(model.Status==AdvertStatus.Active){
                       record.Status = AdvertStatus.Active;
                       await context.SaveAsync(record);
                   }
                   else {
                       await context.DeleteAsync(record);
                   }
                }
            }
        }
    }
}