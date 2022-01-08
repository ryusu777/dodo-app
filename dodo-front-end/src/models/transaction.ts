import { IGoods } from './goods';

export interface ITransactionHeader {
  [index: string]:
    | number
    | Date
    | string
    | undefined
    | Array<ITransactionDetail>;
  id?: number;
  purchaseDate?: Date | string;
  receiveDate?: Date | string;
  createdDate?: Date | string;
  vendor?: string;
  totalPrice?: number;
  transactionType: 'sell' | 'purchase';
  title?: string;
  goodsTransactionDetails?: Array<ITransactionDetail>;
}
export interface ITransactionDetail {
  id?: number;
  goodsTransactionId: number;
  goodsId: number;
  goodsAmount: number;
  pricePerItem: number;
  theGoods: IGoods;
}

export interface ITransactionHeaderFilter {
  purchaseDateFrom: string;
  purchaseDateTo: string;
  receiveDateFrom: string;
  receiveDateTo: string;
}
