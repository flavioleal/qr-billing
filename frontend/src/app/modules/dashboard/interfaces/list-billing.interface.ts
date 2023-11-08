export enum PaymentStatusEnum {
  Pending = 1,
  Canceled = 2,
  Paid = 3
}

export interface ICustomerOutput {
  name: string;
}
export interface IListBillingOutput {
  id: string;
  value: number;
  qrCode: string;
  createdAt: Date;
  status: PaymentStatusEnum;
  customer: ICustomerOutput
  paymentDescription: string;
}
