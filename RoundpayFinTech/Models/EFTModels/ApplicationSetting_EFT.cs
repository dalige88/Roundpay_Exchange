using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoundpayFinTech.Models.EFTModels
{
    public class ApplicationSetting_EFT
    {
        [Key]
        public int _ID { get; set; }
        public bool? _IsMultipleMobileAllowed { get; set; }
        public byte? _RegChargeType { get; set; }
        public bool? _IsDoubleWallet { get; set; }
        public decimal? _CCF_Fixed_Charge { get; set; }
        public bool? _IsCCFApplicable { get; set; }
        public bool? _IsRoleFixed { get; set; }
        public bool? _IsTDSnGSTApplicableRoles { get; set; }
        public bool? _IsGSTEnable { get; set; }
        public bool? _IsRoleHierarchyFixed { get; set; }
        public bool? _IsAPIUserByAll { get; set; }
        public bool? _IsRoleCommissionDisplay { get; set; }
        public bool? _IsPINRequired { get; set; }
        public bool? _IsDTHInfo { get; set; }
        public bool? _IsRoffer { get; set; }
        public bool? _IsHeavyRefresh { get; set; }
        public bool? _IsWhiteLabel { get; set; }
        public bool? _IsPackageAllowed { get; set; }
        public bool? _IsSingleDB { get; set; }
        public byte? _TargetType { get; set; }
        public bool? _IsRealAPIPerTransaction { get; set; }
        public bool? _IsPasswordOnly { get; set; }
        public bool? _IsCommissionByJob { get; set; }
        public bool? _IsAdminFlatComm { get; set; }
        public bool? IsReferral { get; set; }
        public bool? _IsMoveToPrepaid { get; set; }
        public bool? _IsMoveToUtility { get; set; }
        public bool? _IsMoveToBank { get; set; }
        public bool? _IsAPISwitchAfterCircleFailed { get; set; }
        public bool? _IsAPISwitchAfterUserFailed { get; set; }
        public bool? _IsDeductFromPackage { get; set; }
        public bool? _IsPINBlankByDefault { get; set; }
        public bool? _IsRealSettlement { get; set; }
        public bool? _IsDisableDebit { get; set; }
        public bool? _IsFlatCommission { get; set; }
        public bool? _IsDefaultOTPDisabled { get; set; }
        public bool? _IsCommissionOnTopUp { get; set; }
        public bool? _IsGSTDOCTypeEnabled { get; set; }
        public bool? _IsCommissionOnRegistration { get; set; }
        public byte? _IsApproveRequestedAmount { get; set; }
        public bool? _IsDMTWithPIPE { get; set; }
        public bool? _IsProductWiseWalletSet { get; set; }
        public bool? _InvoicePrefix { get; set; }
        public bool? _IsCircleSwitchingFirst { get; set; }
        public bool? _IsLookupFromAPIOnly { get; set; }
        public bool? _IsFlatDisallow { get; set; }
        public bool? _IsGenerateOrderForUPI { get; set; }
        public bool? _IsReferral { get; set; }
        public byte? _FlatID { get; set; }
        public bool? _IsPGLive { get; set; }
        public bool? _IsECommerceCommissionSetByAdmin { get; set; }
        public decimal? _ECommerceCommission { get; set; }
        public bool? _ECommerceCommissionType { get; set; }
        public int? _MinimumBillForFreeShipping { get; set; }
        public bool? _IsAutoBilling { get; set; }
        public bool? _WithCustomLoginID { get; set; }
        public bool? _InDynamicSwitching { get; set; }
        public bool? _IsWLAPIAllowed { get; set; }
        public bool? _IsVirtualAccountInternal { get; set; }
        public bool? _IsWalletForAEPSDynamic { get; set; }
        public byte? _FailoverAPIPriorityID { get; set; }
        public byte? _SettleIDInRegistration { get; set; }
        public bool? _IsMultiCycleInSettle { get; set; }
        public bool? _IsSignupFromAPI { get; set; }
        public byte? _BackupMonth { get; set; }
        public bool? _IsMultiMonthDB { get; set; }
        public decimal? _CertainAmountRedUser { get; set; }
        public decimal? _IsFraudPrevention { get; set; }
        public string _ICICICustomerCode { get; set; }
        public bool? ECommerceDistributionCommissionType { get; set; }
        public bool? _IsAccountStatement { get; set; }
        public bool? _IsB2BAllowed { get; set; }
        public bool? _IsPriceDiffForB2B { get; set; }
        public bool? _RPOnly { get; set; }
        public bool? _IsCashCollectionOnly { get; set; }
        public bool? _IsTDSOfSurchargeUserUpline { get; set; }
        public bool? _IsLowBalanceAlert { get; set; }
        public byte? _MenuBindingLvl { get; set; }
        public bool? _IsRPOnly { get; set; }
        public bool? _IsRechargePlansAPIOnly { get; set; }

    }
}
