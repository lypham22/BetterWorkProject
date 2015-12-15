namespace JobSeeker.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BW.Repository.Data.BWDataContext>
    {
        /// <summary>
        /// Config Auto Migration
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
        /// <summary>
        /// Oversride Seed
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(BW.Repository.Data.BWDataContext context)
        {
            #region Any run code first
            /* base.Seed(context);
            // Create Store Procedure "get-user"
            context.Database.ExecuteSqlCommand(
                    @"Create PROCEDURE [dbo].[get-user]
                    @ID int OUTPUT
                    AS
                    BEGIN
                    SET NOCOUNT ON

                    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                    BEGIN TRANSACTION;

                    BEGIN
                      SELECT *
                      FROM User
                    END  
                    COMMIT TRANSACTION;
                    END");

            * */
            #endregion
        }
    }
}
