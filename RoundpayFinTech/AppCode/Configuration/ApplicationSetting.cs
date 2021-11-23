﻿using Fintech.AppCode.StaticModel;
using RoundpayFinTech.AppCode.StaticModel;

namespace RoundpayFinTech.AppCode.Configuration
{
    public static class ApplicationSetting
    {
        public const bool IsSocialAlert = true;
        public const bool IsGenerateOrderForUPI = false;
        public const bool IsDTHPlanWithChannelList = false; // true Only for Roundpay and Airpay1
        public const bool IsECommerceAllowed = true;
        public const bool IsLowBalanceAlertAllowed = false;
        public const bool IsEmailVefiricationRequired = false;
        public const bool IsWebNotification = false;
        public const bool IsDMTWithPIPE = true;
        public const bool IsTakeCustomerNo = true;
        public const bool IsShopping = false;
        public const bool IsCircleSlabShow = false;//Only true for roundpay
        public const bool IsKYCForced = false;
        public const bool IsErrorCodeEditable = false;
        public const bool IsShowPassword = true;
        public const bool IsWhitelabel = true;
        public const bool IsMultipleMobileAllowed = false;
        public const bool IsDoubleWallet = false;
        public const decimal CCFFixedCharge= 10;
        public const bool IsCCFApplicable = true;
        public const bool IsRoleFixed = true;
        public const bool IsTDSnGSTApplicableRoles = true;
        public const bool IsGSTEnable = false;
        public const bool IsRoleHierarchyFixed = true;
        public const bool IsAPIUserByAll = false;
        public const bool IsRoleCommissionDisplay = true;
        public const int ProjectID = 2750;
        public const bool IsPinRequired = true;
        public const string PlanType = Fintech.AppCode.StaticModel.PlanType.NoPLAN;
        public const bool IsDTHInfo = true;
        public const bool IsDTHInfoCall = true;
        public const bool IsRoffer = true;
        public const bool IsRofferAutoCall = false;
        public const bool IsHeavyRefresh = true;
        public const string FCMKey = "AAAA4ID9cN0:APA91bGE72g6snTREuBFNL0TuRLiUzb8ASq6eZLTXTR4Wfa5g7z5_J5RC6FEYlU6ZXb5y4xB46q9MFKJnM5r8MzWMhodNDHniZbWwNZsefpa4bm1hxB4aqQnAZ44mdokUXzQn0Kd3LnJ";
        public const string FCMSenderID = "964236767453";
        public const string LookUpType = LookupAPIType.NoLOOKUP;
        public const int RegistrationType= RegistrationChargeType.RegIdCount;
        public const bool IsPackageAllowed = true;
        public const bool IsSingleDB = true;
        public const bool IsB2CEnabled = true;
        public const bool IsSalesTeam = true;//If false hide each functionalities related to Employee
        public const bool IsTarget = true;
        public const bool IsDenominationIncentive = true;
        public const int TargetType = Fintech.AppCode.StaticModel.TargetType.Operatorwise;
        public const bool IsShowPDFPlan = true;
        public const bool IsPasswordOnly = false;
        public const bool IsDefaultOTPDisabled = false;
        public const bool IsRealAPIPerTransaction = false;
        public const bool IsCommissionByJob = false;
        public const bool IsPasswordNumeric = false;
        public const bool IsAddMoneyEnable = true;
        
        public const bool IsShowPartner = true;
        public const bool IsFirstHandAPI = false;
        public const bool IsMoveToPrepaid = true;
        public const bool IsMoveToUtility = true;
        public const bool IsMoveToBank = true;
        public const bool IsActivityShow = false;
        public const bool IsShowSubscription = false;
        public const bool IsReferral = true;
        public const bool IsRealSettlement = true;
        public const bool IsDisableDebit = false;
        public const bool IsDisplayCertificate = true;
        public const int ActiveFlatType = FlatTypeMaster.FlatToAll;
        public const bool IsAffiliateRevenue = false;
        public const bool IsGSTDOCTypeEnabled = false;
        public const bool IsApproveRequestedAmount = false;
        public const int ShoppingCommissionType = ShoppingCommType.ProductWise;
        public const bool IsShowChannelSlabBulkSetting = false;
        public static bool IsRPOnly = true;
        public const string GIBLUMC = "";
        public const bool IsECommerceCommissionSetByAdmin = false;
        public const decimal ECommerceCommission = 1.2m; // Applicable in term of IsECommerceCommissionSetByAdmin is true
        public const bool ECommerceCommissionType = true;//true - % & false- fixed ; Applicable in term of IsECommerceCommissionSetByAdmin is true

        public const bool IsPaymentGatway = true;
        public const bool IsUPI = true;
        public const bool IsUPIQR = true;

        public const int ECommerceDistributionCommissionType = CommissionDistributionModal.CommissionShareing;
        public const bool IsLead = false;
        public const bool IsMultipleVedorAllowed = true;
        public const bool IsECollectEnable = true;


        public const string MapKey = "https://maps.googleapis.com/maps/api/js?key=";
        public const string CustomerCodeICICI = "TARF";
        public const bool IsOperatorDailyLimit = false;
        public const bool IsDMTAPIResale = false;
        public const bool IsBBPSAPIResale = false;
        public const bool IsPayoutAPIResale = false;
        
        public const bool IsAEPSAPIResale = false;
        public const bool IsMiniBankAPIResale = false;

        public const bool IsUserwiseDenominationSwitch = true;
        public const bool IsAutoBilling = false;
        public const bool WithCustomLoginID = false;
        public const bool IsLeadServiceEnabled = false;
        public const string ICICIUPIPrivatePFX = "";
        public const bool IsInvoiceByAdmin = false;
        public const bool IsWLAPIAllowed = false;//default condition is false
        public const bool IsVirtualAccountInternal = false;//default condition is false
        public const int FailoverCountID = FailoverApiOrder.FailoverCount;
        public const bool IsMultiCycle = false;
        public const string VLEIDPrefix = "VLE";
        public const bool IsMultiOperatorWithPipe = false;
        public const bool IsSingupPageOff = false;
        public const bool IsAXISCDM = false;
        public const bool IsMultiMonthDB = false;
        public const bool IsFraudPrevention = false;//RoundpayOnly
        public const bool IsUPIPaymentResale = false;
        public const int PrimaryDMTOperator = 11;
        public const bool IsAccountStatement = true;
        public const bool IsBulkFundTransfer = false;

        public const bool IsB2BAllowed = false;
        public const bool IsPriceDiffForB2B = false;
        public const bool IsCashCollectionOnly = false;
        public const bool IsWalletToWallet = false;
        public const bool IsBBPSInStaging = true;
        public const bool IsBirthdayWishAlert = false;
        public const bool IsSpecialCommission = true;
        public const bool IsBillerUpdate = true;
        public const bool IsHostedServerIsIndian = false;
        public const bool IsBankAccountNoReplaceWithSmartCollect = false;
        public const bool IsECommDeliveryAllowed = false;
        public const bool IsPANAPIResale = false;
        public const bool IsWhatsappAgent = true;
        public const string DeliveryFCMKey = "AAAA4JTY7gI:APA91bG-seXsJoIx80kD6BI0lUJO4orXRdH8N9E8jJextSSFaKm3GkYDavTwTO5uP-zad2B-lwYTxy61PiKmS5aUNg4kw47WgFPoQSSuKp-4QdrlgNhyloe-5kEnhavegUNrY3oExhQ7";
        public const string DeliveryFCMSenderID = "964569918978";
        public const bool IsAdditionalService = false;
        public const bool IsBulkQRGeneration = true;
        public const bool MapAXISCDM = true;
        public const bool IsAreaMaster = false;
        public const bool IsRPFintechDMTInsfficientPending = false;
        public const bool IsPlanServiceUpdated = true;
        public const bool IsCOIN = true;
        public const bool IsSattlemntAccountVerify = true;
        public const bool IsIRCTCAPIResale = true;
        public const bool IsDebitWithApproval = true;
        public const bool IsCallbackAlert = true;
        public const bool IsAPIExchange = true;
    }
}
