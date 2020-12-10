import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { IngredientDetailModel, IngredientListModel } from '../api/models';
import { IngredientService } from '../api/services';
import { EnumToArrayPipe } from '../enum-to-array.pipe';

import { RecipeAddIngredientComponent } from './recipe-add-ingredient.component';

describe('RecipeAddIngredientComponent', () => {
  let component: RecipeAddIngredientComponent;
  let fixture: ComponentFixture<RecipeAddIngredientComponent>;

  const ingredientService = jasmine.createSpyObj('IngredientService', ['ingredientGetAll']);
  let getAllSpy: jasmine.Spy;

  const ingredient1: IngredientDetailModel = { id: 'df935095-8709-4040-a2bb-b6f97cb416dc', name: 'Vejce', description: 'Popis vajec' };
  const ingredient2: IngredientDetailModel = { id: '23b3902d-7d4f-4213-9cf0-112348f56238', name: 'Cibule', description: 'Popis cibule' };
  const ingredientsArray: IngredientDetailModel[] = [ingredient1, ingredient2];
  const ingredientsObservable: Observable<Array<IngredientListModel>> = of(ingredientsArray);

  beforeEach(async () => {
    getAllSpy = ingredientService.ingredientGetAll.and.callFake((_x: any) => ingredientsObservable);

    await TestBed.configureTestingModule({
      imports: [FormsModule],
      declarations: [RecipeAddIngredientComponent, EnumToArrayPipe],
      providers: [
        { provide: IngredientService, useValue: ingredientService },
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RecipeAddIngredientComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
