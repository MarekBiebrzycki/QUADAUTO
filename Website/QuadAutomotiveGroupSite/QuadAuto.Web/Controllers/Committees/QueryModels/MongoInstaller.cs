using MongoDB.Bson.Serialization;

namespace QuadAuto.Web.Controllers.Committees.QueryModels
{
    public class MongoInstaller : IMongoInstaller
    {
        public void Install()
        {
            if (!BsonClassMap.IsClassMapRegistered((typeof (CommitteesQueryModel))))
                BsonClassMap.RegisterClassMap<CommitteesQueryModel>(c =>
                                                                        {
                                                                            c.AutoMap();
                                                                            c.SetIsRootClass(true);
                                                                        });
        }
    }
}