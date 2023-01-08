import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalBlogCategoryComponent } from './modal-blog-category.component';

describe('ModalBlogCategoryComponent', () => {
  let component: ModalBlogCategoryComponent;
  let fixture: ComponentFixture<ModalBlogCategoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalBlogCategoryComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalBlogCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
