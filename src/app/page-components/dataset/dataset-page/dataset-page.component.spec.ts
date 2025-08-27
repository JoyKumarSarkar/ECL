import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DatasetPageComponent } from './dataset-page.component';

describe('DatasetPageComponent', () => {
  let component: DatasetPageComponent;
  let fixture: ComponentFixture<DatasetPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DatasetPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DatasetPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
