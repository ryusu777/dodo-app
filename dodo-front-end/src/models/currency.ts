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

export interface ICurrencySummaryPerDay {
  day?: Date;
  fundAmount?: number;
  profitAmount?: number;
  totalFundChange: number;
  totalProfitChange: number;
}
