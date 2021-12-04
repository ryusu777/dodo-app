import { ITransactionHeader } from './transaction.interface';

export interface ICurrency {
  id?: number;
  transactionHeaderId?: number;
  currencyAmount?: number;
  changingAmount?: number;
  dateOfChange?: Date;
  changeDescription?: string;
  theGoodsTransactionHeader?: ITransactionHeader;
}
