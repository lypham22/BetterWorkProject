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
            // Create Store Procedure "seq_nextval_userno"
            context.Database.ExecuteSqlCommand(
                    @"Create PROCEDURE [dbo].[seq_nextval_userno]
                    @ID int OUTPUT
                    AS
                    BEGIN
                    SET NOCOUNT ON

                    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                    BEGIN TRANSACTION;

                    BEGIN
                      INSERT INTO M_訪問先利用者採番(nouse) VALUES (1)
                      IF SCOPE_IDENTITY() > 9999999
                        BEGIN
                          TRUNCATE TABLE [M_訪問先利用者採番];
                          INSERT INTO M_訪問先利用者採番(nouse) VALUES (1)
                        END
                      SELECT @ID = SCOPE_IDENTITY()
                    END  
                    COMMIT TRANSACTION;
                    END");

            // Create Store Procedure "seq_nextval_userno"
            context.Database.ExecuteSqlCommand(
                        @"Create PROCEDURE [dbo].[seq_nextval_visitno]
                        @ID int OUTPUT
                        AS
                        BEGIN
                        SET NOCOUNT ON

                        SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
                        BEGIN TRANSACTION;

                        BEGIN
                          INSERT INTO M_訪問記録採番(nouse) VALUES (1)
                          IF SCOPE_IDENTITY() > 9999999
                            BEGIN
                              TRUNCATE TABLE [M_訪問記録採番];
                              INSERT INTO M_訪問記録採番(nouse) VALUES (1)
                            END
                          SELECT @ID = SCOPE_IDENTITY()
                        END  
                        COMMIT TRANSACTION;
                        END");
            * */
            #endregion
        }
    }
}
