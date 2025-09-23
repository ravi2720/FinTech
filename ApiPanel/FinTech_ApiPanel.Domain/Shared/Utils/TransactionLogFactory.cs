using FinTech_ApiPanel.Domain.DTOs.TransactionLogs;
using FinTech_ApiPanel.Domain.Entities.TransactionLogs;
using FinTech_ApiPanel.Domain.Entities.UserMasters;

namespace FinTech_ApiPanel.Infrastructure.Utilities.TransactionLogs
{
    public static class TransactionLogFactory
    {
        public static TransactionLogDto GetTransactionLogDto(TransactionLog entity, List<UserMaster> userMasters)
        {
            var userInfo = userMasters.FirstOrDefault(x => x.Id == entity.UserId);
            var userName = userInfo.IsAdmin ? "Admin" : userInfo.UserName;
            var userFullName = userInfo.IsAdmin ? "Admin" : userInfo.FullName;

            var refUserInfo = userMasters.FirstOrDefault(x => x.Id == entity.RefUserId);
            string refUser = null;
            if (refUserInfo != null)
                refUser = refUserInfo.IsAdmin ? "Admin" : $"{refUserInfo.FullName}({refUserInfo.UserName})";

            return new TransactionLogDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                UserName = userName,
                UserFullName = userFullName,
                RefUser = refUser,
                ReferenceId = entity.ReferenceId,
                Amount = entity.Amount,
                RemainingAmount = entity.RemainingAmount,
                AuditType = entity.AuditType,
                Status = entity.Status,
                Remark = entity.Remark,

                // Ipay Specific
                OrderId = entity.Ipay_OrderId,
                Timestamp = entity.Ipay_Timestamp ?? entity.CreatedAt,
                OutletId = entity.Ipay_OutletId,
                MobileNumber = entity.MobileNumber,
                Email = entity.Email,
            };
        }
    }
}
