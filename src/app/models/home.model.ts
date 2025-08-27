export interface Return<T> {
  message: string;
  data: T
  success: boolean;
  extraData: any;
}

export interface MenuDetails {
  menuId: number;
  menuItem: string;
  url: string;
  logo: string;
  sequenceNo: number;
  children: MenuDetails[];
}