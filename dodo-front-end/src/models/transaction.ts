import { IGoods } from './goods';

export interface ITransactionHeader {
  id?: number;
  purchaseDate: Date;
  receiveDate: Date;
  createdDate: Date;
  vendor: string;
  totalPrice: number;
  transactionType: 'sell' | 'purchase';
  goodsTransactionDetails: Array<ITransactionDetail>;
}
export interface ITransactionDetail {
  id?: number;
  goodsTransactionId: number;
  goodsId: number;
  goodsAmount: number;
  pricePerItem: number;
  theGoods: IGoods;
}

