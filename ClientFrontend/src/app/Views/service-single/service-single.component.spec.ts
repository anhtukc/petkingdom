import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ServiceSingleComponent } from './service-single.component';

describe('ServiceSingleComponent', () => {
  let component: ServiceSingleComponent;
  let fixture: ComponentFixture<ServiceSingleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ServiceSingleComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ServiceSingleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});