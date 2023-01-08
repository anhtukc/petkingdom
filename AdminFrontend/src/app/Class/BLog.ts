export class Blog {
    id?: string ;
    name: string = null!;
    author: string = null!;
    content: string = null!;
    thumbnail?: string ;
    createdDate?: Date ;
    updateDate?: Date ;
    status: number;
    blogCategoryId?: string ;
    blogCategoryName?: string ;
    file?:File;
  }