import { ITransactionHeader } from './transaction';

export interface ICurrency {
  id?: number;
  transactionHeaderId?: number;
  currencyAmount?: number;
  changingAmount?: number;
  dateOfChange?: Date;
  changeDescription?: string;
  theGoodsTransactionHeader?: ITransactionHeader;
}
