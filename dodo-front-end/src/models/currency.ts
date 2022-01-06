import { ITransactionHeader } from './transaction';

export interface ICurrency {
  id?: number;
  transactionHeaderId?: number;
  profitAmount?: number;
  fundAmount?: number;
  changingProfitAmount?: number;
  changingFundAmount?: number;
  dateOfChange?: Date;
  changeDescription?: string;
  theGoodsTransactionHeader?: ITransactionHeader;
}
