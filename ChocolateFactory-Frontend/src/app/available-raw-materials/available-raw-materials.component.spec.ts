import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AvailableRawMaterialsComponent } from './available-raw-materials.component';

describe('AvailableRawMaterialsComponent', () => {
  let component: AvailableRawMaterialsComponent;
  let fixture: ComponentFixture<AvailableRawMaterialsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AvailableRawMaterialsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AvailableRawMaterialsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
