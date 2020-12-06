import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Observable, of } from 'rxjs';
import { IngredientDetailModel, IngredientListModel } from '../api/models';
import { IngredientService } from '../api/services';

import { IngredientPageComponent } from './ingredient-page.component';

describe('IngredientPageComponent', () => {
  let component: IngredientPageComponent;
  let fixture: ComponentFixture<IngredientPageComponent>;

  const ingredientService = jasmine.createSpyObj('IngredientService', ['ingredientGetAll']);
  let getAllSpy: jasmine.Spy;

  const ingredient1: IngredientDetailModel = { id: 'df935095-8709-4040-a2bb-b6f97cb416dc', name: 'Vejce', description: 'Popis vajec' };
  const ingredient2: IngredientDetailModel = { id: '23b3902d-7d4f-4213-9cf0-112348f56238', name: 'Cibule', description: 'Popis cibule' };
  const ingredientsArray: IngredientDetailModel[] = [ingredient1, ingredient2];
  const ingredientsObservable: Observable<Array<IngredientListModel>> = of(ingredientsArray);

  beforeEach(async () => {
    getAllSpy = ingredientService.ingredientGetAll.and.callFake((_: any) => ingredientsObservable);

    await TestBed.configureTestingModule({
      declarations: [IngredientPageComponent],
      providers: [
        { provide: IngredientService, useValue: ingredientService },
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IngredientPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
