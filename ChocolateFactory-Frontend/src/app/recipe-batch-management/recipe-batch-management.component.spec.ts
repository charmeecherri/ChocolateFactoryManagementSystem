import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecipeBatchManagementComponent } from './recipe-batch-management.component';

describe('RecipeBatchManagementComponent', () => {
  let component: RecipeBatchManagementComponent;
  let fixture: ComponentFixture<RecipeBatchManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RecipeBatchManagementComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RecipeBatchManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
